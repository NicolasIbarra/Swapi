using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;
using MoviesAPI.Infrastructure.Data;

namespace MoviesAPI.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await this.SaveChangesAsync();
            return movie;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _context.Movies.Where(x => x.NullDate == null).ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == id && m.NullDate == null)
                .FirstOrDefaultAsync();
            return movie;
        }

        public async Task<Movie?> GetMoviesByTitleAndEpisode(string title, int episodeId)
        {
            var movie = await _context.Movies
                .Where(m => m.Title.Equals(title) && m.EpisodeId == episodeId)
                .FirstOrDefaultAsync();
            return movie;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SyncMovies(List<Movie> newMovies)
        {
            await _context.Movies.AddRangeAsync(newMovies);
            await this.SaveChangesAsync();
        }
    }
}
