using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MovieApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(ILogger<MoviesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Fetching all movies");
        var movies = GetMovies();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Fetching movie with ID: {Id}", id);
        var movie = GetMovies().FirstOrDefault(m => m.Id == id);
        if (movie == null)
        {
            _logger.LogWarning("Movie with ID: {Id} not found", id);
            return NotFound();
        }

        return Ok(movie);
    }

    private static List<Movie> GetMovies() => new()
    {
        new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Year = 2010 },
        new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", Year = 1972 },
        new Movie { Id = 3, Title = "Interstellar", Genre = "Sci-Fi", Year = 2014 },
        new Movie { Id = 4, Title = "The Dark Knight", Genre = "Action", Year = 2008 }
    };
}

public record Movie
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Genre { get; init; }
    public int Year { get; init; }
}
