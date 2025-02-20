using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Services.LoadServices;

namespace PechkovDenisKt_42_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadsController : ControllerBase
    {
        private readonly LoadService _loadService;

        public LoadsController(LoadService loadService)
        {
            _loadService = loadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoads([FromQuery] string? teacherFirstName, [FromQuery] string? teacherLastName, [FromQuery] string? departmentName, [FromQuery] string? disciplineName)
        {
            var loads = await _loadService.GetLoadsAsync(teacherFirstName, teacherLastName, departmentName, disciplineName);
            return Ok(loads);
        }
    }
}
