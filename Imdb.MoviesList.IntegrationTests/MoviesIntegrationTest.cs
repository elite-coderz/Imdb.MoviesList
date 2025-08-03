using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MovieApiDemo;
using MovieApiDemo.Controllers;
using Xunit;

namespace Imdb.MoviesList.IntegrationTests
{
    public class MoviesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MoviesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithMovieList()
        {
            // Act
            var response = await _client.GetAsync("/api/movies");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var movies = await response.Content.ReadFromJsonAsync<List<Movie>>();
            movies.Should().NotBeNull();
            movies!.Count.Should().Be(4);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk_WithMovie()
        {
            // Act
            var response = await _client.GetAsync("/api/movies/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var movie = await response.Content.ReadFromJsonAsync<Movie>();
            movie.Should().NotBeNull();
            movie!.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/movies/999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
