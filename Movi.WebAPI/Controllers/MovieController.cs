using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MovieController(IMovieService movieService) : ControllerBase
{
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
}
