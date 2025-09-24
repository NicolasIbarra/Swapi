using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesAPI.Api.Controllers;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Tests.TestsData;

namespace MoviesAPI.Tests.Controllers
{
    public class MoviesControllerShould
    {
        /// <summary>
        /// Test para endpoint GetMovies
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetMovies_ReturnOkForMovies()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var firstMovie = MovieTestsData.GetTestMovieOne();
            var secondMovie = MovieTestsData.GetTestMovieTwo();

            var testMovies = new List<Movie>
            {
                firstMovie,
                secondMovie
            };

            movieServiceMock
                .Setup(service => service.GetMovies())
                .ReturnsAsync(testMovies);

            // Act
            var result = await controller.GetMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMovies = Assert.IsType<List<Movie>>(okResult.Value);

            Assert.Equal(2, returnedMovies.Count);
            Assert.Equal("A New Hope", returnedMovies[0].Title);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal("The Empire Strikes Back", returnedMovies[1].Title);
        }

        /// <summary>
        /// Test para endpoint GetMovieById con Id no válido
        /// </summary>
        [Fact]
        public async Task GetMovieById_ReturnBadRequestForInvalidId()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            // Act
            var testId = 0;
            var result = await controller.GetMovieById(testId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(badRequestResult.Value);

            Assert.Equal("El id de la película debe ser válido.", message);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint GetMovieById cuando no existe la película
        /// </summary>
        [Fact]
        public async Task GetMovieById_ReturnNotFoundForNonExistingMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            int movieId = 99;

            movieServiceMock
                .Setup(service => service.GetMovieById(movieId))
                .ReturnsAsync((Movie)null);

            // Act
            var result = await controller.GetMovieById(movieId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint GetMovieById cuando la película sí existe
        /// </summary>
        [Fact]
        public async Task GetMovieById_ReturnOkForExistingMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var testMovie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMovieById(testMovie.Id))
                .ReturnsAsync(testMovie);

            // Act
            var result = await controller.GetMovieById(testMovie.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMovie = Assert.IsType<Movie>(okResult.Value);

            Assert.Equal(testMovie.Id, returnedMovie.Id);
            Assert.Equal("A New Hope", returnedMovie.Title);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint CreateMovie cuando la película es nula
        /// </summary>
        [Fact]
        public async Task CreateMovie_ReturnBadRequestForNullMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            Movie? movie = null;

            // Act
            var result = await controller.CreateMovie(movie);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint CreateMovie cuando la película ya existe
        /// </summary>
        [Fact]
        public async Task CreateMovie_ReturnConflictForExistingMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var movie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMoviesByTitleAndEpisode(movie.Title, movie.EpisodeId))
                .ReturnsAsync(movie);

            // Act
            var result = await controller.CreateMovie(movie);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);
            Assert.Equal("La película ya existe.", conflictResult.Value);
        }

        /// <summary>
        /// Test para endpoint CreateMovie cuando la película es creada correctamente
        /// </summary>
        [Fact]
        public async Task CreateMovie_ReturnOkForCreatedMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var movie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMoviesByTitleAndEpisode(movie.Title, movie.EpisodeId))
                .ReturnsAsync((Movie?)null);

            movieServiceMock
                .Setup(service => service.CreateMovie(movie))
                .ReturnsAsync(movie);

            // Act
            var result = await controller.CreateMovie(movie);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdMovie = Assert.IsType<Movie>(okResult.Value);

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(movie.Title, createdMovie.Title);
            Assert.Equal(movie.EpisodeId, createdMovie.EpisodeId);
        }

        /// <summary>
        /// Test para endpoint UpdateMovieById cuando el Id no es válido
        /// </summary>
        [Fact]
        public async Task UpdateMovieById_ReturnBadRequestForInvalidId()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            int invalidId = -1; // ID inválido
            var movie = MovieTestsData.GetTestMovieOne();

            // Act
            var result = await controller.UpdateMovieById(invalidId, movie);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("El id de la película debe ser válido.", badRequestResult.Value);
        }

        /// <summary>
        /// Test para endpoint UpdateMovieById cuando la película no existe.
        /// </summary>
        [Fact]
        public async Task UpdateMovieById_ReturnNotFoundForNonExistingMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            int nonExistingMovieId = 999; // ID que no existe
            var movie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMovieById(nonExistingMovieId))
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await controller.UpdateMovieById(nonExistingMovieId, movie);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint UpdateMovieById cuando la actualización falla
        /// </summary>
        [Fact]
        public async Task UpdateMovieById_ReturnProblemWhenUpdateFails()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var firstMovie = MovieTestsData.GetTestMovieOne();
            var secondMovie = MovieTestsData.GetTestMovieTwo();

            movieServiceMock
                .Setup(service => service.GetMovieById(secondMovie.Id))
                .ReturnsAsync(secondMovie);

            movieServiceMock
                .Setup(service => service.UpdateMovie(firstMovie, secondMovie))
                .ReturnsAsync(false); 

            // Act
            var result = await controller.UpdateMovieById(secondMovie.Id, firstMovie);

            // Assert
            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, problemResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint UpdateMovieById cuando la actualización es exitosa.
        /// </summary>
        [Fact]
        public async Task UpdateMovieById_ReturnNoContentForSuccessfulUpdate()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var firstMovie = MovieTestsData.GetTestMovieOne();
            var secondMovie = MovieTestsData.GetTestMovieTwo();

            movieServiceMock
                .Setup(service => service.GetMovieById(secondMovie.Id))
                .ReturnsAsync(secondMovie);

            movieServiceMock
                .Setup(service => service.UpdateMovie(firstMovie, secondMovie))
                .ReturnsAsync(true);

            // Act
            var result = await controller.UpdateMovieById(secondMovie.Id, firstMovie);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint DeleteMovieById cuando el Id no es válido
        /// </summary>
        [Fact]
        public async Task DeleteMovieById_ReturnBadRequestForInvalidId()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            int testId = -1; 

            // Act
            var result = await controller.DeleteMovieById(testId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("El id de la película debe ser válido.", badRequestResult.Value);
        }

        /// <summary>
        /// Test para endpoint DeleteMovieById cuando la película no existe
        /// </summary>
        [Fact]
        public async Task DeleteMovieById_ReturnNotFoundForNonExistingMovie()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            int testId = 999;

            movieServiceMock
                .Setup(service => service.GetMovieById(testId))
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await controller.DeleteMovieById(testId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint DeleteMovieById cuando la eliminación falla
        /// </summary>
        [Fact]
        public async Task DeleteMovieById_ReturnProblemWhenDeleteFails()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var movie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMovieById(movie.Id))
                .ReturnsAsync(movie);

            movieServiceMock
                .Setup(service => service.DeleteMovie(movie))
                .ReturnsAsync(false); 

            // Act
            var result = await controller.DeleteMovieById(movie.Id);

            // Assert
            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, problemResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint DeleteMovieById cuando la eliminación es exitosa
        /// </summary>
        [Fact]
        public async Task DeleteMovieById_ReturnNoContentForSuccessfulDelete()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var movie = MovieTestsData.GetTestMovieOne();

            movieServiceMock
                .Setup(service => service.GetMovieById(movie.Id))
                .ReturnsAsync(movie);

            movieServiceMock
                .Setup(service => service.DeleteMovie(movie))
                .ReturnsAsync(true); 

            // Act
            var result = await controller.DeleteMovieById(movie.Id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint SynchronizeMovies cuando la sincronización es exitosa
        /// </summary>
        [Fact]
        public async Task SynchronizeMovies_ReturnOkForSuccessfulSynchronization()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var controller = new MoviesController(movieServiceMock.Object);

            var movieList = new List<Movie>
            {
                new Movie { Id = 1, Title = "A New Hope", EpisodeId = 4, Director = "George Lucas" },
                new Movie { Id = 2, Title = "The Empire Strikes Back", EpisodeId = 5, Director = "Irvin Kershner" }
            };

            movieServiceMock
                .Setup(service => service.SyncMovies())
                .ReturnsAsync(movieList); 

            // Act
            var result = await controller.SyncMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<Movie>>(okResult.Value);

            Assert.Equal(2, movies.Count);
            Assert.Equal("A New Hope", movies[0].Title); 
        }
    }
}
