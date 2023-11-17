using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SSquared.App.DTO;
using SSquared.App.Extensions;
using SSquared.Lib.Data.Entities;
using SSquared.Lib.Exceptions;
using SSquared.Lib.Repositories;
using SSquared.Lib.Services;

namespace SSquared.App.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/Employees")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeRepository employeeRepository, IManagerService managerValidationService, IOrgChartService orgChartService)
        {
            _employeeRepository = employeeRepository;
            _managerService = managerValidationService;
            _orgChartService = orgChartService;
        }

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IManagerService _managerService;
        private readonly IOrgChartService _orgChartService;

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

        [HttpGet("{supervisorId}/Employees")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetEmployeesForSupervisor(int supervisorId)
        {
            var supervisor = await _employeeRepository.GetAsync(supervisorId, HttpContext.RequestAborted);
            if (supervisor is null)
            {
                return NotFound();
            }

            var dtos = supervisor
                .Employees
                .Select(e => e.ToEmployeeDto(Url))
                .ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}/OrgChart")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetEmployeeOrgChart(int id)
        {
            var employee = await _employeeRepository.GetAsync(id, HttpContext.RequestAborted);
            if (employee is null)
            {
                return NotFound();
            }

            var orgChart = await _orgChartService.GetOrgChartForEmployeeAsync(employee, HttpContext.RequestAborted);
            var dto = orgChart.ToOrgChartNodeDto();

            return Ok(dto);
        }

        [HttpGet("{id}/PotentialManagers")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetPotentialManagers(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(id, HttpContext.RequestAborted);
                if (employee is null)
                {
                    return NotFound();
                }

                var potentialManagers = await _managerService.GetValidManagersForEmployee(employee, HttpContext.RequestAborted);
                var dtos = potentialManagers
                    .Select(pm => pm.ToEmployeeDto(Url))
                    .OrderBy(pm => pm.FirstName)
                    .ThenBy(pm => pm.LastName);

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
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
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

                var managerIsValid = await _managerService.MayBeManagedByAsync(
                    employee: existingEmployee,
                    potentialManager: manager,
                    cancellationToken: HttpContext.RequestAborted);
                if (!managerIsValid)
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
