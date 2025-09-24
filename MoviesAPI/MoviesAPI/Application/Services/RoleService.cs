using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;

namespace MoviesAPI.Application.Services
{
    /// <summary>
    /// Servicio de roles
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Obtiene los roles existentes
        /// </summary>
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();
            return roles;
        }

        /// <summary>
        /// Obtiene un rol por su id
        /// </summary>
        /// <param name="id"> Id del rol </param>
        public async Task<Role?> GetRoleById(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            return role;
        }
    }
}
