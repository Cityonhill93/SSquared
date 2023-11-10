using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SSquared.App.Extensions;
using SSquared.Lib.Repositories;

namespace SSquared.App.Controllers
{
    [ApiController]
    [ResponseCache(Duration = 300)] //5 minutes
    [Route("api/{v:apiVersion}/Roles")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class RoleController : Controller
    {
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        private readonly IRoleRepository _roleRepository;

        [HttpGet("{id}")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleRepository.GetAsync(id, HttpContext.RequestAborted);
            if (role is null)
            {
                return NotFound();
            }

            var dto = role.ToRoleDto();

            return Ok(dto);
        }

        [HttpGet("")]
        [ApiVersion("1")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleRepository.GetAsync(HttpContext.RequestAborted);
            var dtos = roles
                .Select(r => r.ToRoleDto())
                .ToList();

            return Ok(dtos);
        }
    }
}
