using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.DisciplineFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PechkovDenisKt_42_22.Services.DisciplineServices
{
    public class DisciplineService
    {
        private readonly UniversityContext _context;

        public DisciplineService(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<DisciplineFilter>> GetDisciplinesAsync(string firstName = null, string lastName = null, int? minHours = null, int? maxHours = null)
        {
            var query = _context.Disciplines
                .Include(d => d.Loads)
                .ThenInclude(l => l.Teacher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(d => d.Loads.Any(l => l.Teacher.FirstName.Contains(firstName)));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(d => d.Loads.Any(l => l.Teacher.LastName.Contains(lastName)));
            }

            if (minHours.HasValue)
            {
                query = query.Where(d => d.Loads.Sum(l => l.Hours) >= minHours.Value);
            }

            if (maxHours.HasValue)
            {
                query = query.Where(d => d.Loads.Sum(l => l.Hours) <= maxHours.Value);
            }

            var disciplines = await query.Select(d => new DisciplineFilter
            {
                Id = d.Id,
                Name = d.Name,
                TotalHours = d.Loads.Sum(l => l.Hours),
                Teachers = d.Loads.Select(l => $"{l.Teacher.FirstName} {l.Teacher.LastName}").ToList()
            }).ToListAsync();

            return disciplines;
        }
    }
}
