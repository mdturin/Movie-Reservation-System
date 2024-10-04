using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;

namespace Movi.Infrastructure.Repositories;

public class MovieRepository(ApplicationDbContext context)
    : BulkRepository(context), IMovieRepository
{
    public Task<List<Movie>> GetMoviesAsync()
    {
        var dbSet = GetDbSetAsNoTrackingQueryable<Movie>();
        return dbSet
            .Include(m => m.Showtimes)
            .Include(m => m.Cast)
            .ToListAsync();
    }

    public Task<List<Movie>> GetMoviesWithShowTimes(DateTime date)
    {
        var dbSet = GetDbSetAsNoTrackingQueryable<Movie>();
        return dbSet
            .Include(m => m.Showtimes)
            .Where(m => m.Showtimes.Any(s => s.StartTime.Equals(date)))
            .ToListAsync();
    }

    public Task<List<Movie>> GetMoviesWithShowTimes(IEnumerable<string> genres)
    {
        var dbSet = GetDbSetAsNoTrackingQueryable<Movie>();
        return dbSet
            .Where(m => genres.Any(g => m.Genre.ToLower().Contains(g)))
            .Include(m => m.Showtimes)
            .ToListAsync();
    }
}
