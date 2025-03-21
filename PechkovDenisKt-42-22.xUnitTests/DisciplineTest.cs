using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Models.DTO;
using PechkovDenisKt_42_22.Services.DisciplineServices;

namespace PechkovDenisKt_42_22.xUnitTests
{
    public class DisciplineServiceTests
    {
        private readonly DbContextOptions<UniversityContext> _dbContextOptions;

        public DisciplineServiceTests()
        {
            var dbName = $"TestDatabase_Discipline_{Guid.NewGuid()}"; 
            _dbContextOptions = new DbContextOptionsBuilder<UniversityContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task AddDisciplineAsync_AddsDisciplineSuccessfully()
        {
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);
                var disciplineDto = new DisciplineDto { Name = "New Discipline" };

                var result = await disciplineService.AddDisciplineAsync(disciplineDto);

                var addedDiscipline = await ctx.Disciplines.FindAsync(result.Id);
                Assert.NotNull(addedDiscipline);
                Assert.Equal("New Discipline", addedDiscipline.Name);
            }
        }

        [Fact]
        public async Task UpdateDisciplineAsync_UpdatesDisciplineSuccessfully()
        {
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);
                var disciplineDto = new DisciplineDto { Name = "Old Discipline" };

                var addedDiscipline = await disciplineService.AddDisciplineAsync(disciplineDto);
                var updateDto = new DisciplineDto { Name = "Updated Discipline" };

                var updatedDiscipline = await disciplineService.UpdateDisciplineAsync(addedDiscipline.Id, updateDto);

                Assert.NotNull(updatedDiscipline);
                Assert.Equal("Updated Discipline", updatedDiscipline.Name);
            }
        }

        [Fact]
        public async Task DeleteDisciplineAsync_DeletesDisciplineSuccessfully()
        {
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);
                var disciplineDto = new DisciplineDto { Name = "Discipline to Delete" };

                var addedDiscipline = await disciplineService.AddDisciplineAsync(disciplineDto);
                var result = await disciplineService.DeleteDisciplineAsync(addedDiscipline.Id);

                Assert.True(result);
                var deletedDiscipline = await ctx.Disciplines.FindAsync(addedDiscipline.Id);
                Assert.Null(deletedDiscipline);
            }
        }

        [Fact]
        public async Task GetDisciplinesAsync_ReturnsFilteredDisciplines()
        {
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);

                
                var department = new Department { Name = "Department of Science" };
                await ctx.Departments.AddAsync(department);
                await ctx.SaveChangesAsync();

                var discipline1 = new Discipline { Name = "Mathematics" };
                var discipline2 = new Discipline { Name = "Physics" };

                
                var teacher1 = new Teacher { FirstName = "John", LastName = "Doe", Department = department };
                var teacher2 = new Teacher { FirstName = "Jane", LastName = "Doe", Department = department };

                var load1 = new Load { Teacher = teacher1, Discipline = discipline1, Hours = 10 };
                var load2 = new Load { Teacher = teacher2, Discipline = discipline2, Hours = 20 };

                await ctx.Disciplines.AddRangeAsync(new[] { discipline1, discipline2 });
                await ctx.Teachers.AddRangeAsync(new[] { teacher1, teacher2 });
                await ctx.Loads.AddRangeAsync(new[] { load1, load2 });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);
                var result = await disciplineService.GetDisciplinesAsync("John");

                Assert.Single(result);
                Assert.Equal("Mathematics", result.First().Name);
            }
        }

        [Fact]
        public async Task GetDisciplineByIdAsync_ReturnsDiscipline()
        {
            using (var ctx = new UniversityContext(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(ctx);
                var disciplineDto = new DisciplineDto { Name = "Chemistry" };

                var addedDiscipline = await disciplineService.AddDisciplineAsync(disciplineDto);
                var result = await disciplineService.GetDisciplineByIdAsync(addedDiscipline.Id);

                Assert.NotNull(result);
                Assert.Equal("Chemistry", result.Name);
            }
        }
    }
}