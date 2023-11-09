using Microsoft.AspNetCore.Mvc;

namespace SSquared.App.Controllers
{
    [Route("api/Employees")]
    public class EmployeeController
    {
        [HttpGet("{id}")]
        public Task<IActionResult> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("")]
        public Task<IActionResult> GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
