using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Enums;

namespace MoviesAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Obtiene los roles existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        [Authorize(Roles = RolesEnum.AdminUser)]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return Ok(roles);
        }

        /// <summary>
        /// Obtiene un rol por su id
        /// </summary>
        /// <param name="id"> Id del rol </param>
        /// <returns></returns>
        [HttpGet("GetRoleById")]
        [Authorize(Roles = RolesEnum.RegularUser)]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id debe ser válido.");
            }

            var role = await _roleService.GetRoleById(id);

            if (role == null)
            {
                return NotFound("El rol no existe.");
            }

            return Ok(role);
        }
    }
}
