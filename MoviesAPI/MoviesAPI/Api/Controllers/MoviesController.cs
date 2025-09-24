using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Enums;

namespace MoviesAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Obtener las películas existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMovies")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            var movies = await _movieService.GetMovies();
            return Ok(movies);
        }

        /// <summary>
        /// Busca una película por su Id
        /// </summary>
        /// <param name="id"> Id de la película a buscar </param>
        /// <returns></returns>
        [HttpGet("GetMovieById/{id}")]
        [Authorize(Roles = RolesEnum.RegularUser)]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id de la película debe ser válido.");
            }

            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Crea una nueva película
        /// </summary>
        /// <param name="movie"> Nueva película </param>
        /// <returns></returns>
        [HttpPost("CreateMovie")]
        [Authorize(Roles = RolesEnum.AdminUser)]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();    
            }

            bool existsMovie = (await _movieService.GetMoviesByTitleAndEpisode(movie.Title, movie.EpisodeId) != null);

            if (existsMovie)
            {
                return Conflict("La película ya existe.");
            }

            var newMovie = await _movieService.CreateMovie(movie);
            return Ok(newMovie);
        }

        /// <summary>
        /// Actualiza la información de una película existente
        /// </summary>
        /// <param name="id"> Id de la película </param>
        /// <param name="movie"> Datos de la película </param>
        /// <returns></returns>
        [HttpPut("UpdateMovieById/{id}")]
        [Authorize(Roles = RolesEnum.AdminUser)]
        public async Task<IActionResult> UpdateMovieById(int id, Movie movie)
        {
            if (id <= 0)
            {
                return BadRequest("El id de la película debe ser válido.");
            }

            if (movie == null)
            {
                return BadRequest("Los datos de la película deben ser válidos.");
            }

            var existingMovie = await _movieService.GetMovieById(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            var result = await _movieService.UpdateMovie(movie, existingMovie);

            if (!result)
            {
                return Problem();
            }

            return NoContent();
        }

        /// <summary>
        /// Borra una película existente
        /// </summary>
        /// <param name="id"> Id de la película a borrar </param>
        /// <returns></returns>
        [HttpDelete("DeleteMovieById/{id}")]
        [Authorize(Roles = RolesEnum.AdminUser)]
        public async Task<IActionResult> DeleteMovieById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id de la película debe ser válido.");
            }

            var existingMovie = await _movieService.GetMovieById(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            var result = await _movieService.DeleteMovie(existingMovie);

            if (!result)
            {
                return Problem();
            }

            return NoContent();
        }

        /// <summary>
        /// Sincroniza las películas desde la API pública de SWAPI
        /// </summary>
        /// <returns> Listado de nuevas películas añadidas </returns>
        [HttpPost("SynchronizeMovies")]
        [Authorize(Roles = RolesEnum.AdminUser)]
        public async Task<ActionResult<List<Movie>?>> SyncMovies()
        {
            var result = await _movieService.SyncMovies();

            return Ok(result);
        }
    }
}
