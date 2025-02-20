using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.TeacherFilters;


namespace PechkovDenisKt_42_22.Services.TeacherServices
{

    public class TeacherService
    {
        private readonly UniversityContext _context;

        public TeacherService(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherFilter>> GetTeachersAsync(string departmentName = null, string degreeName = null, string positionName = null)
        {
            var query = _context.Teachers
                .Include(t => t.Degree)
                .Include(t => t.Position)
                .Include(t => t.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(departmentName))
            {
                query = query.Where(t => t.Department.Name == departmentName);
            }

            if (!string.IsNullOrEmpty(degreeName))
            {
                query = query.Where(t => t.Degree.Name == degreeName);
            }

            if (!string.IsNullOrEmpty(positionName))
            {
                query = query.Where(t => t.Position.Name == positionName);
            }

            var teachers = await query.Select(t => new TeacherFilter
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Degree = t.Degree.Name,
                Position = t.Position.Name,
                Department = t.Department.Name
            }).ToListAsync();

            return teachers;
        }
    }


}
