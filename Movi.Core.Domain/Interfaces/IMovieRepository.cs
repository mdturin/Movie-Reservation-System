using System.Linq.Expressions;
using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieRepository : IBulkRepository
{
    Task<List<Movie>> GetMoviesAsync();
    Task<List<Movie>> GetMoviesAsync(Expression<Func<Movie, bool>> exp);
}
