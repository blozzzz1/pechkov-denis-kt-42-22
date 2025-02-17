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
        public Task<Teacher[]> GetTeachersByNameAsync(TeacherNameFilter filter, CancellationToken cancellationToken);

    }

    public class TeacherService : ITeacherService
    {
        private readonly UniversityContext _dbContext;

        public TeacherService(UniversityContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Teacher[]> GetTeachersByNameAsync(TeacherNameFilter filter, CancellationToken cancellationToken = default)
        {
            
            var teachers = await _dbContext.Set<Teacher>()
               .Where(w => w.FirstName == filter.FirstName)
               .ToArrayAsync(cancellationToken);

            return teachers;
        }
    }


}
