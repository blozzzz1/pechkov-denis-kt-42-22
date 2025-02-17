using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.TeacherFilters;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Controllers;


namespace PechkovDenisKt_42_22.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken); 
    }

    public class TeacherService : ITeacherService
    {
        private readonly UniversityContext _dbContext;

        public TeacherService(UniversityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Teacher>().AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(t => t.FirstName.Contains(filter.FirstName));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(t => t.LastName.Contains(filter.LastName));
            }

            if (filter.DegreeId.HasValue)
            {
                query = query.Where(t => t.DegreeId == filter.DegreeId.Value);
            }

            if (filter.PositionId.HasValue)
            {
                query = query.Where(t => t.PositionId == filter.PositionId.Value);
            }

            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(t => t.DepartmentId == filter.DepartmentId.Value);
            }

            return await query.ToArrayAsync(cancellationToken); // Это все еще будет работать, так как IEnumerable<Teacher> может быть создан из массива
        }
    }


}
