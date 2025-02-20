using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Services.TeacherServices;
using PechkovDenisKt_42_22.Filters.TeacherFilters;


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
    }
}
