using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// Id del usuario
        /// </summary>
        [Key] 
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        [Required]
        public string Username { get; set; } = null!;

        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        [Required]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Rol del usuario
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}
