using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SSquared.App.Extensions;
using SSquared.Lib.Repositories;

namespace SSquared.App.Controllers
{
    [ApiController]
    [Route("api/{v:apiVersion}/Employees")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private readonly IEmployeeRepository _employeeRepository;

        [HttpGet("{id}")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetAsync(id, HttpContext.RequestAborted);
            if (employee is null)
            {
                return NotFound();
            }

            var dto = employee.ToExpandedEmployeeDto(Url);
            return Ok(dto);
        }

        [HttpGet("")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetEmployees(string? filter = null)
        {
            var employees = await _employeeRepository.GetAsync(filter, HttpContext.RequestAborted);
            var dtos = employees
                .Select(e => e.ToEmployeeDto(Url))
                .ToList();

            return Ok(dtos);
        }
    }
}
