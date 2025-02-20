using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.DepartmentFilters;


namespace PechkovDenisKt_42_22.Services.DepartmentServices
{
    public class DepartmentService
    {
        private readonly UniversityContext _context;

        public DepartmentService(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentFilter>> GetDepartmentsAsync(DateTime? foundedAfter = null, int? minTeacherCount = null)
        {
            var query = _context.Departments
                .Include(d => d.Head)
                .Include(d => d.Teachers)
                .AsQueryable();

            if (foundedAfter.HasValue)
            {
                query = query.Where(d => d.FoundedDate >= foundedAfter.Value);
            }

            if (minTeacherCount.HasValue)
            {
                query = query.Where(d => d.Teachers.Count >= minTeacherCount.Value);
            }

            var departments = await query.Select(d => new DepartmentFilter
            {
                Id = d.Id,
                Name = d.Name,
                FoundedDate = d.FoundedDate,
                Head = d.Head != null ? $"{d.Head.FirstName} {d.Head.LastName}" : "Нет заведующего",
                TeacherCount = d.Teachers.Count
            }).ToListAsync();

            return departments;
        }
    }

}
