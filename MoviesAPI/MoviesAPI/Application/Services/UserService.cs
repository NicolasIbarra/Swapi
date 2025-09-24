using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;

namespace MoviesAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly AuthService _authService;

        public UserService(IUserRepository userRepository, IRoleService roleService, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _roleService = roleService;
        }

        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        public async Task<User> RegisterUser(User user)
        {
            user.Password = _authService.EncryptPassword(user.Password);
            var newUser = await _userRepository.RegisterUser(user);
            return newUser;
        }

        /// <summary>
        /// Busca un usuario por su nombre de usuario
        /// </summary>
        public async Task<User?> SearchUserByUsername(string username)
        {
            var user = await _userRepository.SearchUserByUsername(username);
            return user;
        }

        /// <summary>
        /// Busca un usuario por su nombre de usuario y contraseña
        /// </summary>
        public async Task<object> SearchUserByUsernameAndPassword(string username, string password)
        {
            var passHash = _authService.EncryptPassword(password);
            var loggedUser = await _userRepository.SearchUserByUsernameAndPassword(username, passHash);

            if (loggedUser == null)
            {
                return new
                {
                    IsSuccess = false,
                    Token = "",
                    Message = "Credenciales incorrectas."
                };
            }

            var userRole = await _roleService.GetRoleById(loggedUser.RoleId);
            var userToken = _authService.GenerateJWT(loggedUser, userRole.Name);

            return new
            {
                IsSuccess = true,
                Token = userToken,
                Message = "Login exitoso."
            };
        }

        /// <summary>
        /// Busca un usuario por su correo electrónico
        /// </summary>
        public async Task<User?> SearchUserByEmail(string email)
        {
            var user = await _userRepository.SearchUserByEmail(email);
            return user;
        }
    }
}
