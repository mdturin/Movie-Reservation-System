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

        return GetDbSetAsNoTrackingQueryable<Seat>()
            .Where(s => s.IsAvailable && s.ShowtimeId == showtimeId)
            .ToListAsync();
    }

    public Task<List<Seat>> GetAvailableSeatsAsync(IEnumerable<string> seatNumbers)
    {
        return GetDbSetAsNoTrackingQueryable<Seat>()
            .Where(s => s.IsAvailable && seatNumbers.Contains(s.SeatNumber))
            .ToListAsync();
    }
}
