using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Application.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Crea una nueva película
        /// </summary>
        Task<Movie> CreateMovie(Movie movie);

        /// <summary>
        /// Borra una película existente
        /// </summary>
        Task<bool> DeleteMovie(Movie existingMovie);

        /// <summary>
        /// Obtener las películas existentes
        /// </summary>
        Task<List<Movie>> GetMovies();

        /// <summary>
        /// Busca una película por su Id
        /// </summary>
        Task<Movie?> GetMovieById(int id);

        /// <summary>
        /// Busca una película por título y episodio
        /// </summary>
        Task<Movie?> GetMoviesByTitleAndEpisode(string title, int episodeId);

        /// <summary>
        /// Actualiza la información de una película existente
        /// </summary>
        Task<bool> UpdateMovie(Movie movie, Movie existingMovie);

        /// <summary>
        /// Sincroniza las películas desde la API pública de SWAPI
        /// </summary>
        Task<List<Movie>?> SyncMovies();
    }
}
