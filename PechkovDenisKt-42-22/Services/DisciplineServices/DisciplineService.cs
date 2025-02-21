﻿using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Database;
using PechkovDenisKt_42_22.Filters.DisciplineFilters;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Models.DTO;
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
                Teachers = d.Loads
                    .Select(l => $"{l.Teacher.FirstName} {l.Teacher.LastName}")
                    .Distinct()
                    .ToList()
            }).ToListAsync();

            return disciplines;
        }

        public async Task<Discipline> GetDisciplineByIdAsync(int id)
        {
            return await _context.Disciplines
                .Include(d => d.Loads)
                .ThenInclude(l => l.Teacher)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discipline> AddDisciplineAsync(DisciplineDto disciplineDto)
        {
            var discipline = new Discipline
            {
                Name = disciplineDto.Name
            };

            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();
            return discipline;
        }

        public async Task<Discipline> UpdateDisciplineAsync(int id, DisciplineDto disciplineDto)
        {
            var existingDiscipline = await _context.Disciplines.FindAsync(id);
            if (existingDiscipline == null)
            {
                throw new KeyNotFoundException("Discipline not found");
            }

            existingDiscipline.Name = disciplineDto.Name;

            await _context.SaveChangesAsync();
            return existingDiscipline;
        }

        public async Task<bool> DeleteDisciplineAsync(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return false;
            }

            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
