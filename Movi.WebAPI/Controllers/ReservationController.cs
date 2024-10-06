using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

public class ReservationController(IBulkRepository context, ISeatService seatService) : AControllerBase
{
    private readonly IBulkRepository _context = context;
    private readonly ISeatService _seatService = seatService;

    [HttpPost]
    public async Task<IActionResult> ReserveSeats([FromBody] ReservationDto request)
    {
        using var session = _context.BeginTransaction();
        var seats = await _seatService.GetAvailableSeatsAsync(request.SeatIds);
        if (seats.Count != request.SeatIds.Count)
        {
            return BadRequest("Some seats are not available");
        }

        var reservations = new List<Reservation>();
        seats.ForEach(seat =>
        {
            seat.IsAvailable = false;
            var reservation = new Reservation()
            {
                UserId = request.UserId,
                SeatId = seat.Id,
                ShowtimeId = seat.ShowtimeId,
                ReservedAt = DateTime.UtcNow
            };

            reservations.Add(reservation);
        });

        await _context.AddAsync(reservations);
        await session.CommitAsync();

        return Ok("Seats reserved successfully");
    }
}
