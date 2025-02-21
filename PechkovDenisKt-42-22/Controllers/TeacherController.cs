using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Services.TeacherServices;
using PechkovDenisKt_42_22.Filters.TeacherFilters;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Models.DTO;


namespace PechkovDenisKt_42_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers([FromQuery] string? departmentName, [FromQuery] string? degreeName, [FromQuery] string? positionName)
        {
            var teachers = await _teacherService.GetTeachersAsync(departmentName, degreeName, positionName);
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound($"Учитель с идентификатором {id} не найден.");
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherDto teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest("Invalid teacher data.");
            }

            var teacherResponse = await _teacherService.AddTeacherAsync(
                teacherDto.FirstName,
                teacherDto.LastName,
                teacherDto.PositionId,
                teacherDto.DegreeId,
                teacherDto.DepartmentId
            );

            return CreatedAtAction(nameof(AddTeacher), new { id = teacherResponse.Id }, teacherResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest("Invalid teacher data.");
            }

            var teacherResponse = await _teacherService.UpdateTeacherAsync(
                id,
                teacherDto.FirstName,
                teacherDto.LastName,
                teacherDto.PositionId,
                teacherDto.DegreeId,
                teacherDto.DepartmentId
            );

            if (teacherResponse == null)
            {
                return NotFound();
            }

            return Ok(teacherResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }
    }
}
