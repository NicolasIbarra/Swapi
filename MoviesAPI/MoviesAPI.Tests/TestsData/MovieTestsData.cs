using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Tests.TestsData
{
    public static class MovieTestsData
    {
        public static Movie GetTestMovieOne()
        {
            return new Movie
            {
                Id = 1,
                Title = "A New Hope",
                EpisodeId = 4,
                OpeningCrawl = "It is a period of civil war...",
                Director = "George Lucas",
                Producers = new List<string> { "Gary Kurtz", "George Lucas" },
                ReleaseDate = new DateTime(1977, 5, 25),
                Species = new List<string> { "Human", "Droid" },
                Starships = new List<string> { "X-wing", "TIE Fighter" },
                Vehicles = new List<string> { "Sand Crawler" },
                Characters = new List<string> { "Luke Skywalker", "Leia Organa" },
                Planets = new List<string> { "Tatooine", "Alderaan" },
                Url = "https://swapi.dev/api/films/1/",
                Created = DateTime.UtcNow,
                Edited = DateTime.UtcNow
            };
        }

        public static Movie GetTestMovieTwo()
        {
            return new Movie
            {
                Id = 2,
                Title = "The Empire Strikes Back",
                EpisodeId = 5,
                OpeningCrawl = "It is a dark time for the Rebellion...",
                Director = "Irvin Kershner",
                Producers = new List<string> { "Gary Kurtz" },
                ReleaseDate = new DateTime(1980, 5, 21),
                Species = new List<string> { "Human", "Wookiee" },
                Starships = new List<string> { "Millennium Falcon", "Star Destroyer" },
                Vehicles = new List<string> { "AT-AT" },
                Characters = new List<string> { "Han Solo", "Darth Vader" },
                Planets = new List<string> { "Hoth", "Dagobah" },
                Url = "https://swapi.dev/api/films/2/",
                Created = DateTime.UtcNow,
                Edited = DateTime.UtcNow
            };
        }
    }
}
