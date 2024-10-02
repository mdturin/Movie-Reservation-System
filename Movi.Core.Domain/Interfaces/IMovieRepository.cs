using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieRepository : IBulkRepository
{
    Task<List<Movie>> GetMoviesWithShowTimes(DateTime date);
    Task<List<Movie>> GetMoviesAsync();
}
