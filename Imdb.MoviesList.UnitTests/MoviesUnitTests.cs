using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieApiDemo.Controllers;
using System.Linq;
using Xunit;

namespace Imdb.MoviesList.UnitTests
{
    public class MoviesControllerTests
    {
        private readonly Mock<ILogger<MoviesController>> _mockLogger;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockLogger = new Mock<ILogger<MoviesController>>();
            _controller = new MoviesController(_mockLogger.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfMovies()
        {
            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(okResult.Value);
            Assert.Equal(5, movies.Count());
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult_WithMovie()
        {
            // Arrange
            int movieId = 1;

            // Act
            var result = _controller.GetById(movieId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movie = Assert.IsType<Movie>(okResult.Value);
            Assert.Equal(movieId, movie.Id);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int movieId = 999;

            // Act
            var result = _controller.GetById(movieId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

