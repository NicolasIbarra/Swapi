using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Application.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Obtiene los roles existentes
        /// </summary>
        Task<List<Role>> GetRoles();

        /// <summary>
        /// Obtiene un rol por su id
        /// </summary>
        /// <param name="id"> Id del rol </param>
        Task<Role?> GetRoleById(int id);
    }
}
