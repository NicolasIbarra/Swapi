using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Domain.Entities
{
    public class Movie
    {
        /// <summary>
        /// Id de la película
        /// </summary>
        [Key]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        /// <summary>
        /// Titulo de la película
        /// </summary>
        [Required]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Id del episodio de la película
        /// </summary>
        [Required]
        public int EpisodeId { get; set; }

        /// <summary>
        /// Texto de apertura de la película
        /// </summary>
        public string OpeningCrawl { get; set; } = null!;

        /// <summary>
        /// Director de la película
        /// </summary>
        [Required]
        public string Director { get; set; } = null!;

        /// <summary>
        /// Productores de la película
        /// </summary>
        [Required]
        public List<string> Producers { get; set; } = null!;

        /// <summary>
        /// Fecha de lanzamiento de la película
        /// </summary>
        [Required]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Especies que aparecen en la película
        /// </summary>
        public List<string> Species { get; set; } = null!;

        /// <summary>
        /// Naves espaciales que aparecen en la película
        /// </summary>
        public List<string> Starships { get; set; } = null!;

        /// <summary>
        /// Vehiculos que aparecen en la película
        /// </summary>
        public List<string> Vehicles { get; set; } = null!;

        /// <summary>
        /// Personajes que aparecen en la película
        /// </summary>
        [Required]
        public List<string> Characters { get; set; } = null!;

        /// <summary>
        /// Planetas que aparecen en la película
        /// </summary>
        public List<string> Planets { get; set; } = null!;

        /// <summary>
        /// Url del recurso
        /// </summary>
        public string Url { get; set; } = null!;

        /// <summary>
        /// Fecha de creación del recurso
        /// </summary>
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// Fecha de edición del recurso
        /// </summary>
        public DateTime Edited { get; set; }

        /// <summary>
        /// Fecha de baja 
        /// </summary>
        [SwaggerSchema(ReadOnly = true)]
        public DateTime? NullDate { get; set; }
    }
}
