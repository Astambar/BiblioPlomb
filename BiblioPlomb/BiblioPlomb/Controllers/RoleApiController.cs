using BiblioPlomb.Services;
using Microsoft.AspNetCore.Mvc;
using BiblioPlomb.Models;

namespace BiblioPlomb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleApiController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleApiController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string type)
        {
            try
            {
                var role = await _roleService.CreateRoleAsync(type);
                return CreatedAtAction(nameof(GetRoleByType), new { type = role.Type }, role);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetRoleByType(string type)
        {
            var role = await _roleService.GetRoleByTypeAsync(type);
            if (role == null)
            {
                return NotFound($"Le rôle '{type}' n'existe pas.");
            }
            return Ok(role);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound($"Le rôle avec l'id '{id}' n'existe pas.");
            }
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRoles([FromQuery] string query)
        {
            var roles = await _roleService.SearchRolesByTypeAsync(query);
            return Ok(roles);
        }
    }
}
