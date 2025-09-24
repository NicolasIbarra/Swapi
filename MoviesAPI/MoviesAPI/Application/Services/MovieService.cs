using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MoviesAPI.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IConfiguration _configuration;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IConfiguration configuration, IMovieRepository movieRepository)
        {
            _configuration = configuration;
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Crea una nueva película
        /// </summary>
        public async Task<Movie> CreateMovie(Movie movie)
        {
            var newMovie = await _movieRepository.CreateMovie(movie);
            return newMovie;
        }

        /// <summary>
        /// Borra una película existente
        /// </summary>
        public async Task<bool> DeleteMovie(Movie existingMovie)
        {
            existingMovie.NullDate = DateTime.Now;
            await _movieRepository.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Obtener las películas existentes
        /// </summary>
        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _movieRepository.GetMovies();
            return movies;
        }

        /// <summary>
        /// Busca una película por su Id
        /// </summary>
        public async Task<Movie?> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            return movie;
        }

        /// <summary>
        /// Busca una película por título y episodio
        /// </summary>
        public async Task<Movie?> GetMoviesByTitleAndEpisode(string title, int episodeId)
        {
            var movie = await _movieRepository.GetMoviesByTitleAndEpisode(title, episodeId);
            return movie;
        }

        /// <summary>
        /// Actualiza la información de una película existente
        /// </summary>
        public async Task<bool> UpdateMovie(Movie movie, Movie existingMovie)
        {
            existingMovie.Title = movie.Title;
            existingMovie.EpisodeId = movie.EpisodeId;
            existingMovie.OpeningCrawl = movie.OpeningCrawl;
            existingMovie.Director = movie.Director;
            existingMovie.Producers = movie.Producers;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.Species = movie.Species;
            existingMovie.Starships = movie.Starships;
            existingMovie.Vehicles = movie.Vehicles;
            existingMovie.Characters = movie.Characters;
            existingMovie.Planets = movie.Planets;
            existingMovie.Url = movie.Url;
            existingMovie.Created = movie.Created;
            existingMovie.Edited = movie.Edited;

            await _movieRepository.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Sincroniza las películas desde la API pública de SWAPI
        /// </summary>
        public async Task<List<Movie>?> SyncMovies()
        {
            var apiMovies = await GetApiMovies();
            List<Movie> convertedMovies = ConvertMovies(apiMovies);
            List<Movie> existingMovies = await GetMovies();

            var newMovies = convertedMovies
                .Where(cm =>
                    !existingMovies.Any(em =>
                        em.Title == cm.Title 
                        && em.EpisodeId == cm.EpisodeId
                        && em.NullDate == null))
                .ToList();

            if (newMovies.Count != 0)
            {
                await _movieRepository.SyncMovies(newMovies);
            }

            return newMovies;
        }

        /// <summary>
        /// Obtiene las peliículas desde la API pública de SWAPI
        /// </summary>
        /// <returns></returns>
        private async Task<dynamic?> GetApiMovies()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = _configuration["Swapi:GetAllFilms"]!;
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);
                    return data["result"];

                } else
                {
                    return new List<Movie>();  
                }
            }
        }

        /// <summary>
        /// Convierte las plículas obtenidas a objetos Movie
        /// </summary>
        /// <param name="apiMovies"></param>
        /// <returns></returns>
        private static List<Movie> ConvertMovies(dynamic apiMovies)
        {
            var movies = new List<Movie>();

            if (apiMovies != null)
            {
                foreach (var film in apiMovies)
                {
                    var newMovie = new Movie
                    {
                        Title = film["properties"]["title"],
                        EpisodeId = film["properties"]["episode_id"],
                        OpeningCrawl = film["properties"]["opening_crawl"],
                        Director = film["properties"]["director"],
                        Producers = new List<string>((film["properties"]["producer"]).ToString().Split(',')),
                        ReleaseDate = DateTime.Parse(film["properties"]["release_date"].ToString()),
                        Species = ((JArray)film["properties"]["species"]).ToObject<List<string>>(),
                        Starships = ((JArray)film["properties"]["starships"]).ToObject<List<string>>(),
                        Vehicles = ((JArray)film["properties"]["vehicles"]).ToObject<List<string>>(),
                        Characters = ((JArray)film["properties"]["characters"]).ToObject<List<string>>(),
                        Planets = ((JArray)film["properties"]["planets"]).ToObject<List<string>>(),
                        Url = film["properties"]["url"],
                        Created = DateTime.Parse(film["properties"]["created"].ToString()),
                        Edited = DateTime.Parse(film["properties"]["edited"].ToString())
                    };

                    movies.Add(newMovie);
                }
            }
            return movies;
        }
    }
}
