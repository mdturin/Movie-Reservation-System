using System.Linq.Expressions;
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

    public Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> exp)
    {
        return GetDbSetAsNoTrackingQueryable<Movie>()
            .Include(m => m.Showtimes)
            .Where(exp)
            .ToListAsync();
    }
}
