using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;

namespace Movi.Infrastructure.Repositories;

public class ShowtimeRepository(ApplicationDbContext context)
    : BulkRepository(context), IShowtimeRepository
{
    public Task<Showtime> GetShowtimeAsync(string id)
    {
        var dbSet = GetDbSet<Showtime>();
        return dbSet
            .AsNoTracking()
            .Include(s => s.CinemaHall)
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<List<Showtime>> GetShowtimesAsync()
    {
        return GetDbSet<Showtime>()
            .AsNoTracking()
            .Include(s => s.CinemaHall)
            .Include(s => s.Movie)
            .ToListAsync();
    }
}
