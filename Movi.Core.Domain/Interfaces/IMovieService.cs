using Movi.Core.Domain.Dtos;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieService
{
    Task<int> AddAsync(MovieDto dto);
    Task<int> UpdateAsync(MovieDto dto);
}
