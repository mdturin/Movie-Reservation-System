using System.Linq.Expressions;
using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieRepository : IBulkRepository
{
    Task<List<Movie>> GetMoviesWithShowTimes(DateTime date);
    Task<List<Movie>> GetMoviesWithShowTimes(IEnumerable<string> genres);
    Task<List<Movie>> GetMoviesAsync();
    Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> exp);
}
