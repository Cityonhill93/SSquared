using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SSquared.App.DTO;
using SSquared.App.Extensions;
using SSquared.Lib.Data.Entities;
using SSquared.Lib.Exceptions;
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
            if (!employee.IsValid())
            {
                return BadRequest();
            }

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

        [HttpGet("{id}/PotentialManagers")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetPotentialManagers(int id)
        {
            try
            {
                var potentialManagers = await _employeeRepository.GetPotentialManagersAsync(id, HttpContext.RequestAborted);
                var dtos = potentialManagers.Select(pm => pm.ToEmployeeDto(Url));

                return Ok(dtos);
            }
            catch (NotFoundException<Employee>)
            {
                return NotFound();
            }
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

        [HttpPut("{id}")]
        [ApiVersion("1")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] ModifyEmployeeDto employee)
        {
            if (!employee.IsValid())
            {
                return BadRequest();
            }

            var existingEmployee = await _employeeRepository.GetAsync(id, HttpContext.RequestAborted);
            if (existingEmployee is null)
            {
                return NotFound();
            }

            if (employee.ManagerId is not null)
            {
                //Let's make sure that it is a legit employee
                var manager = await _employeeRepository.GetAsync(employee.ManagerId.Value, HttpContext.RequestAborted);
                if (manager is null)
                {
                    return BadRequest("Invalid manager ID");
                }
                else if (!existingEmployee.MayBeManagedBy(manager))
                {
                    return BadRequest("Invalid manager!");
                }
            }

            var updatedEmployee = await _employeeRepository.UpdateAsync(
                id: id,
                args: employee.ToModifyEmployeeArguments(),
                HttpContext.RequestAborted);
            var dto = updatedEmployee.ToExpandedEmployeeDto(Url);

            return Ok(dto);

        }
    }
}
