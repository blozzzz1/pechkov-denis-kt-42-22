using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Interfaces.TeacherInterfaces;
using PechkovDenisKt_42_22.Filters.TeacherFilters;


namespace PechkovDenisKt_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherService _teacherService;

        public TeacherController(ILogger<TeacherController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost("search", Name = "SearchTeachers")]
        public async Task<IActionResult> SearchTeachers(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetTeachersAsync(filter, cancellationToken);
            return Ok(teachers);
        }
    }
}
