using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.LoadFilters;


namespace PechkovDenisKt_42_22.Services.LoadServices
{
    public class LoadService
    {
        private readonly UniversityContext _context;

        public LoadService(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<LoadFilter>> GetLoadsAsync(string teacherFirstName = null, string teacherLastName = null, string departmentName = null, string disciplineName = null)
        {
            var query = _context.Loads
                .Include(l => l.Teacher)
                .ThenInclude(t => t.Department)
                .Include(l => l.Discipline)
                .AsQueryable();

            if (!string.IsNullOrEmpty(teacherFirstName))
            {
                query = query.Where(l => l.Teacher.FirstName.Contains(teacherFirstName));
            }

            if (!string.IsNullOrEmpty(teacherLastName))
            {
                query = query.Where(l => l.Teacher.LastName.Contains(teacherLastName));
            }

            if (!string.IsNullOrEmpty(departmentName))
            {
                query = query.Where(l => l.Teacher.Department.Name.Contains(departmentName));
            }

            if (!string.IsNullOrEmpty(disciplineName))
            {
                query = query.Where(l => l.Discipline.Name.Contains(disciplineName));
            }

            var loads = await query.Select(l => new LoadFilter
            {
                Id = l.Id,
                TeacherName = $"{l.Teacher.FirstName} {l.Teacher.LastName}",
                DepartmentName = l.Teacher.Department.Name,
                DisciplineName = l.Discipline.Name,
                Hours = l.Hours
            }).ToListAsync();

            return loads;
        }
    }
}