using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;

namespace Movi.Infrastructure.Repositories;

public class MovieRepository(ApplicationDbContext context)
    : BulkRepository(context), IMovieRepository
{
    public Task<List<Movie>> GetMoviesAsync(Expression<Func<Movie, bool>> conditionExpression)
    {
        return GetDbSetAsNoTrackingQueryable<Movie>()
            .Include(m => m.Showtimes)
            .Where(conditionExpression)
            .ToListAsync();
    }
}
