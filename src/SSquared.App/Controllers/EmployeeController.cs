using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SSquared.App.DTO;
using SSquared.App.Extensions;
using SSquared.Lib.Repositories;

namespace SSquared.App.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/Employees")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private readonly IEmployeeRepository _employeeRepository;

        [HttpPost("")]
        [ApiVersion("1")]
        public async Task<IActionResult> AddEmployee([FromBody] ModifyEmployeeDto employee)
        {
            if (employee.ManagerId is not null)
            {
                //Let's make sure that it is a legit employee
                var manager = await _employeeRepository.GetAsync(employee.ManagerId.Value, HttpContext.RequestAborted);
                if (manager is null)
                {
                    return BadRequest("Invalid manager ID");
                }
            }

            var createdEmployee = await _employeeRepository.AddAsync(
                args: employee.ToModifyEmployeeArguments(),
                cancellationToken: HttpContext.RequestAborted);
            var createdDto = createdEmployee.ToExpandedEmployeeDto(Url);

            return Ok(createdDto);
        }

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
