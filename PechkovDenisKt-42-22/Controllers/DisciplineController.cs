using Microsoft.AspNetCore.Mvc;
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
    }
}