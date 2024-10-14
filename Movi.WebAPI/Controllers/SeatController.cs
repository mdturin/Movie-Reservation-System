using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

public class SeatController(
    IMapper mapper, ISeatRepository context) : AControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISeatRepository _context = context;

    [HttpGet]
    public async Task<IActionResult> Seats()
    {
        return Ok(await _context.GetAllAsync<Seat>() ?? []);
    }

    [HttpGet("available")]
    public async Task<IActionResult> AvailableSeats([FromQuery] string showTimeId)
    {
        var seats = await _context
            .GetAvailableSeatsByShowTimeIdAsync(showTimeId);
        return Ok(_mapper.Map<List<SeatDto>>(seats));
    }
}
