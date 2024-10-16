using System.Linq.Expressions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieService
{
    Task AddAsync(MovieDto dto);
    Task UpdateAsync(MovieDto dto);
    Task DeleteAsync(string id);
    Task<List<MovieDto>> GetMoviesAsync(Expression<Func<Movie, bool>> exp);
}
