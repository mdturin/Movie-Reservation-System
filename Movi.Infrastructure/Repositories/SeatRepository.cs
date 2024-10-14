using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;

namespace Movi.Infrastructure.Repositories;

public class SeatRepository(ApplicationDbContext context)
    : BulkRepository(context), ISeatRepository
{
    public Task<List<Seat>> GetAvailableSeatsByShowTimeIdAsync(string showtimeId)
    {
        if (string.IsNullOrWhiteSpace(showtimeId))
            return Task.FromResult(new List<Seat>());

        var now = DateTime.UtcNow;
        return GetDbSetAsNoTrackingQueryable<Seat>()
            .Where(s => s.IsAvailable &&
                s.ShowtimeId == showtimeId &&
                s.Showtime != null &&
                (s.Showtime.StartTime - now).TotalMinutes >= 180
            )
            .ToListAsync();
    }

    public Task<List<Seat>> GetAvailableSeatsAsync(
        string showtimeId, IEnumerable<string> seatNumbers)
    {
        var now = DateTime.UtcNow;
        return GetDbSetAsNoTrackingQueryable<Seat>()
            .Include(s => s.Showtime)
            .Where(s =>
                s.IsAvailable &&
                s.ShowtimeId == showtimeId &&
                s.Showtime != null &&
                (s.Showtime.StartTime - now).TotalMinutes >= 180 &&
                seatNumbers.Contains(s.SeatNumber)
            )
            .ToListAsync();
    }
}
