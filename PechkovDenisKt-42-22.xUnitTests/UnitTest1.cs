using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Services.DepartmentServices;

namespace PechkovDenisKt_42_22.xUnitTests {
    public class DepartmentServiceTests
    {
        private readonly DbContextOptions<UniversityContext> _dbContextOptions;

        public DepartmentServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;


            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }


        [Fact]
        public async Task GetDepartmentsAsync_FoundedAfter_ReturnsFilteredDepartments()
        {
            
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var departmentService = new DepartmentService(ctx);

                var departments = new List<Department>
            {
                new Department
                {
                    Name = "Department A",
                    FoundedDate = new DateTime(2020, 1, 1)
                },
                new Department
                {
                    Name = "Department B",
                    FoundedDate = new DateTime(2021, 1, 1)
                },
                new Department
                {
                    Name = "Department C",
                    FoundedDate = new DateTime(2019, 1, 1)
                }
            };

                await ctx.Departments.AddRangeAsync(departments);
                await ctx.SaveChangesAsync();
            }

            
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var departmentService = new DepartmentService(ctx);
                var foundedAfter = new DateTime(2020, 1, 1);
                var result = await departmentService.GetDepartmentsAsync(foundedAfter, null);

                
                Assert.Equal(2, result.Count);
                Assert.Contains(result, d => d.Name == "Department A");
                Assert.Contains(result, d => d.Name == "Department B");
                Assert.DoesNotContain(result, d => d.Name == "Department C");
            }
        }

        [Fact]
        public async Task GetDepartmentsAsync_MinTeacherCount_ReturnsFilteredDepartments()
        {
            
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var departmentService = new DepartmentService(ctx);

                var degree = new Degree { Name = "PhD" };
                var position = new Position { Name = "Professor" }; 

                await ctx.Degrees.AddAsync(degree);
                await ctx.Positions.AddAsync(position);
                await ctx.SaveChangesAsync();

                var departmentWithTeachers = new Department
                {
                    Name = "Department A",
                    FoundedDate = DateTime.Now,
                    Teachers = new List<Teacher>
                {
                    new Teacher
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        DegreeId = degree.Id,
                        PositionId = position.Id,
                        DepartmentId = null 
                    },
                    new Teacher
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        DegreeId = degree.Id,
                        PositionId = position.Id,
                        DepartmentId = null 
                    }
                }
                };

                var departmentWithoutTeachers = new Department
                {
                    Name = "Department B",
                    FoundedDate = DateTime.Now
                };

                await ctx.Departments.AddRangeAsync(new[] { departmentWithTeachers, departmentWithoutTeachers });
                await ctx.SaveChangesAsync();
            }

            
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var departmentService = new DepartmentService(ctx);
                var result = await departmentService.GetDepartmentsAsync(null, 2);

                
                Assert.Single(result);
                Assert.Equal("Department A", result.First().Name);
            }
        }
    }
}