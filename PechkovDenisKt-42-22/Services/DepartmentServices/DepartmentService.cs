using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.DepartmentFilters;
using PechkovDenisKt_42_22.Models;


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

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                return null;
            }

            existingDepartment.Name = department.Name;
            existingDepartment.FoundedDate = department.FoundedDate;
            existingDepartment.HeadId = department.HeadId; 

            await _context.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            var department = await _context.Departments
                .Include(d => d.Teachers)
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
            {
                return false; 
            }

            
            department.HeadId = null;
            await _context.SaveChangesAsync();

            
            _context.Teachers.RemoveRange(department.Teachers);
            await _context.SaveChangesAsync();

            
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
