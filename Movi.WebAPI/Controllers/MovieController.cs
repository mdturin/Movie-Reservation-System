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
        if (await _movieService.AddAsync(movie) > 0)
            return Ok("Movie was successfully added.");
        return BadRequest("Failed to add movie!");
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMovie(MovieDto movie)
    {
        if (await _movieService.UpdateAsync(movie) > 0)
            return Ok("Movie was successfully updated.");
        return BadRequest("Failed to update movie!");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMovie(string movieId)
    {
        try
        {
            await _movieService.DeleteAsync(movieId);
            return Ok("Movie was successfully deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetMovies([FromQuery] GetMovieQueryParams queryParams)
    {
        return Ok(await _movieService.GetMoviesAsync(queryParams.ToExpression()));
    }
}
