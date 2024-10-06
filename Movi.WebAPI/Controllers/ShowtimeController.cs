using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Application.Conditions;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

public class ShowtimeController(IShowtimeRepository context, IMapper mapper) : AControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IShowtimeRepository _context = context;

    [HttpPost]
    public async Task<IActionResult> CreateShowtime([FromBody] ShowtimeDto input)
    {
        var cinemaHall = await _context
            .GetByIdAsync<CinemaHall>(input.CinemaHallId);
        var movie = await _context.GetByIdAsync<Movie>(input.MovieId);

        if (cinemaHall == null)
            return NotFound($"CinemaHall not found with id({input.CinemaHallId})");

        if (movie == null)
            return NotFound($"Movie not found with id({input.MovieId})");

        var showTime = new Showtime
        {
            StartTime = input.StartTime,
            CinemaHallId = cinemaHall.Id,
            MovieId = movie.Id
        };

        await _context.AddAsync(showTime);
        return CreatedAtAction(nameof(GetShowtime), new { id = showTime.Id }, showTime);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShowtime(string id)
    {
        var showTime = await _context.GetShowtimeAsync(id);
        if (showTime == null)
            return NotFound($"Showtime not found with id({id})");

        return Ok(_mapper.Map<ShowtimeDto>(showTime));
    }

    [HttpGet("{showtimeId}/seats")]
    public async Task<IActionResult> GetAvailableSeats(string showtimeId)
    {
        var fieldCondition = new FieldCondition<Seat>(nameof(Seat.ShowtimeId), showtimeId);
        var seats = await _context.GetItemsAsync(fieldCondition.ToExpression());
        return Ok(_mapper.Map<List<SeatDto>>(seats));
    }
}
