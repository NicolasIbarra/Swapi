using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Infrastructure.Data;

namespace MoviesAPI.Domain.Repositories
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Crea una nueva película
        /// </summary>
        /// <param name="movie"> Datos de nueva película </param>
        /// <returns></returns>
        public Task<Movie> CreateMovie(Movie movie);

        /// <summary>
        /// Obtener las películas existentes
        /// </summary>
        /// <returns></returns>
        public Task<List<Movie>> GetMovies();

        /// <summary>
        /// Busca una película por su Id
        /// </summary>
        /// <param name="id"> Id de la pelicula </param>
        /// <returns></returns>
        public Task<Movie?> GetMovieById(int id);

        /// <summary>
        /// Busca una película por título y episodio
        /// </summary>
        /// <param name="title"></param>
        /// <param name="episodeId"></param>
        /// <returns></returns>
        public Task<Movie?> GetMoviesByTitleAndEpisode(string title, int episodeId);

        /// <summary>
        /// Persiste los cambios en la base de datos
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync();


        /// <summary>
        /// Finaliza la sincronización de películas
        /// </summary>
        /// <param name="newMovies"></param>
        /// <returns></returns>
        public Task SyncMovies(List<Movie> newMovies);
    }
}
