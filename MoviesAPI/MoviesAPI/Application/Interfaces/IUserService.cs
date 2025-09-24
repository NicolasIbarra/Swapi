using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        /// <param name="user"> Información del nuevo usuario </param>
        Task<User> RegisterUser(User user);

        /// <summary>
        /// Busca un usuario por su nombre de usuario
        /// </summary>
        Task<User?> SearchUserByUsername(string username);

        /// <summary>
        /// Busca un usuario por su nombre de usuario y contraseña
        /// </summary>
        Task<object> SearchUserByUsernameAndPassword(string username, string passwordHash);

        /// <summary>
        /// Busca un usuario por su correo electrónico
        /// </summary>
        Task<User?> SearchUserByEmail(string email);
    }
}
