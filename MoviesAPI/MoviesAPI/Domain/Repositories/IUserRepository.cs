using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        /// <param name="user"> Datos de nuevo usuario </param>
        /// <returns></returns>
        public Task<User> RegisterUser(User user);

        /// <summary>
        /// Busca un usuario por su nombre de usuario
        /// </summary>
        /// <param name="username"> Nombre de usuario a buscar </param>
        /// <returns></returns>
        public Task<User?> SearchUserByUsername(string username);

        /// <summary>
        /// Busca un usuario por su nombre de usuario y contraseña
        /// </summary>
        /// <param name="username"> Nombre de usuario a buscar </param>
        /// <param name="passwordHash"> Hash de contraseña del usuario </param>
        /// <returns></returns>
        public Task<User?> SearchUserByUsernameAndPassword(string username, string passwordHash);

        /// <summary>
        /// Busca un usuario por su email
        /// </summary>
        /// <param name="email"> Email a buscar </param>
        /// <returns></returns>
        public Task<User?> SearchUserByEmail(string email);
    }
}
