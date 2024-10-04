using Movi.Core.Domain.Dtos;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieService
{
    Task<int> AddAsync(MovieDto dto);
    Task<int> UpdateAsync(MovieDto dto);
    Task DeleteAsync(string id);
    Task<List<MovieDto>> GetMovies();
    Task<List<MovieDto>> GetMoviesWithShowTimes(DateTime date);
    Task<List<MovieDto>> GetMoviesWithShowTimes(IEnumerable<string> genres);
}
