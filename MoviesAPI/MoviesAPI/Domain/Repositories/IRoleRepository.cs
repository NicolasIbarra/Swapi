using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Domain.Repositories
{
    /// <summary>
    /// Repositorio de roles
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Obtiene los roles existentes
        /// </summary>
        /// <returns></returns>
        public Task<List<Role>> GetRoles();

        /// <summary>
        /// Busca un rol por su id
        /// </summary>
        /// <param name="id"> Id del rol </param>
        /// <returns></returns>
        public Task<Role?> GetRoleById(int id);
    }
}
