using Microsoft.AspNetCore.Mvc;
using PechkovDenisKt_42_22.Services.DepartmentServices;
using System;
using System.Threading.Tasks;

namespace PechkovDenisKt_42_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] DateTime? foundedAfter, [FromQuery] int? minTeacherCount)
        {
            var departments = await _departmentService.GetDepartmentsAsync(foundedAfter, minTeacherCount);
            return Ok(departments);
        }
    }
}