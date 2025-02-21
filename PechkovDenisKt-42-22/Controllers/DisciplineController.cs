using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Filters.DisciplineFilters;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Models.DTO;
using PechkovDenisKt_42_22.Services.DisciplineServices;
using System.Threading.Tasks;

namespace PechkovDenisKt_42_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly DisciplineService _disciplineService;

        public DisciplinesController(DisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplines([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] int? minHours, [FromQuery] int? maxHours)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(firstName, lastName, minHours, maxHours);
            return Ok(disciplines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisciplineById(int id)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }

            var disciplineFilter = new DisciplineFilter
            {
                Id = discipline.Id,
                Name = discipline.Name,
                TotalHours = discipline.Loads.Sum(l => l.Hours),
                Teachers = discipline.Loads.Select(l => $"{l.Teacher.FirstName} {l.Teacher.LastName}").ToList()
            };

            return Ok(disciplineFilter);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscipline([FromBody] DisciplineDto disciplineDto)
        {
            if (disciplineDto == null || string.IsNullOrEmpty(disciplineDto.Name))
            {
                return BadRequest("Discipline is null or name is empty.");
            }

            var createdDiscipline = await _disciplineService.AddDisciplineAsync(disciplineDto);
            return CreatedAtAction(nameof(GetDisciplineById), new { id = createdDiscipline.Id }, createdDiscipline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscipline(int id, [FromBody] DisciplineDto disciplineDto)
        {
            if (disciplineDto == null || string.IsNullOrEmpty(disciplineDto.Name))
            {
                return BadRequest("Discipline is null or name is empty.");
            }

            try
            {
                var updatedDiscipline = await _disciplineService.UpdateDisciplineAsync(id, disciplineDto);
                return Ok(updatedDiscipline);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id)
        {
            var result = await _disciplineService.DeleteDisciplineAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}