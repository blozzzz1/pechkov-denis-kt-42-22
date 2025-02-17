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

        [HttpPost(Name = "GetTeachersByName")]
        public async Task<IActionResult> GetTeachersByName(TeacherNameFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetTeachersByNameAsync(filter, cancellationToken);
            return Ok(teachers);
        }
    }
}
