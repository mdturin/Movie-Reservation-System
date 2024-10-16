using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Interfaces;
using Movi.WebAPI.Params;

namespace Movi.WebAPI.Controllers;

public class MovieController(IMovieService movieService, ILogger<MovieController> logger)
    : AControllerBase
{
    private readonly ILogger<MovieController> _logger = logger;
    private readonly IMovieService _movieService = movieService;

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddMovie(MovieDto movie)
    {
        await _movieService.AddAsync(movie);
        return Ok("Movie was successfully added.");
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMovie(MovieDto movie)
    {
        await _movieService.UpdateAsync(movie);
        return Ok("Movie was successfully updated.");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMovie(string movieId)
    {
        await _movieService.DeleteAsync(movieId);
        return Ok("Movie was successfully deleted.");
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetMovies([FromQuery] GetMovieQueryParams queryParams)
    {
        return Ok(await _movieService.GetMoviesAsync(queryParams.ToExpression()));
    }
}
