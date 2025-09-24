using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Services;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        /// <param name="user"> Información del nuevo usuario </param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            bool existsUsername = (await _userService.SearchUserByUsername(user.Username) != null);
            bool existsEmail = (await _userService.SearchUserByEmail(user.Email) != null);

            if (existsUsername || existsEmail)
            {
                return Conflict("El nombre de usuario o correo ya existen.");
            }

            var newUser = await _userService.RegisterUser(user);
            return Ok(newUser);
        }

        /// <summary>
        /// Autenticación de un usuario
        /// </summary>
        /// <param name="username"> Usuario </param>
        /// <param name="password"> Contraseña </param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            if(username == null || password == null)
            {
                return BadRequest("Ingrese credenciales válidas.");
            }

            var loggedUser = await _userService.SearchUserByUsernameAndPassword(username, password);

            if (loggedUser == null)
            {
                return NotFound("Credenciales incorrectas.");
            }

            return Ok(loggedUser);
        }
    }
}
