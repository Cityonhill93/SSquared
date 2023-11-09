using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SSquared.App.Controllers
{
    [ApiController]
    [Route("api/{v:apiVersion}/Employees")]
    public class EmployeeController
    {
        [HttpGet("{id}")]
        [ApiVersion("1")]
        public Task<IActionResult> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("")]
        [ApiVersion("1")]
        public Task<IActionResult> GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
