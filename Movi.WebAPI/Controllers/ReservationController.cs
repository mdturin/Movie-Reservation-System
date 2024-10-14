using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Application.Conditions;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

public class ReservationController(
    IMapper mapper,
    ISeatRepository context) : AControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISeatRepository _context = context;

    [HttpPost]
    public async Task<IActionResult> ReserveSeats([FromBody] ReservationDto request)
    {
        using var session = _context.BeginTransaction();
        var seats = await _context
            .GetAvailableSeatsAsync(request.ShowtimeId, request.SeatNumbers);

        if (seats.Count != request.SeatNumbers.Count)
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
        await _context.UpdateAsync(seats);
        await session.CommitAsync();

        return Ok("Seats reserved successfully");
    }

    [HttpDelete]
    public async Task<IActionResult> CancleSeats([FromBody] ReservationDto request)
    {
        using var session = _context.BeginTransaction();
        var seats = await _context.GetAvailableSeatsAsync(request.SeatNumbers);
        if (seats.Count != request.SeatNumbers.Count)
        {
            return BadRequest("Some seats are not found reserved!");
        }

        var seatIdCondition = new FieldCondition<Reservation>(nameof(Reservation.SeatId), null);
        var userIdCondition = new FieldCondition<Reservation>(nameof(Reservation.UserId), request.UserId);
        var condition = new AndCondition<Reservation>(seatIdCondition, userIdCondition);

        seats.ForEach(async seat =>
        {
            seatIdCondition.Value = seat.Id;
            var reservation = await _context
                .GetItemAsync(condition.ToExpression());
            if (reservation == null) return;
            await _context.DeleteAsync<Reservation>(reservation.Id);
            seat.IsAvailable = true;
        });

        await _context.UpdateAsync(seats);
        await session.CommitAsync();

        return Ok("Seats reservation cancled successfully");
    }
}
