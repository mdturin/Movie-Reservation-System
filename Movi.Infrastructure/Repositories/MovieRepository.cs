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
        return GetDbSet<Movie>()
            .AsNoTracking()
            .AsQueryable()
            .Include(m => m.Showtimes)
            .Include(m => m.Cast)
            .ToListAsync();
    }

    public Task<List<Movie>> GetMoviesWithShowTimes(DateTime date)
    {
        var dbSet = GetDbSet<Movie>();
        return dbSet
            .AsNoTracking()
            .AsQueryable()
            .Include(m => m.Showtimes)
            .Where(m => m.Showtimes.Any(s => s.StartTime.Equals(date)))
            .ToListAsync();
    }
}
