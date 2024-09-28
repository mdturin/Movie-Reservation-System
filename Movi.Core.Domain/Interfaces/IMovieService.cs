using Movi.Core.Domain.Dtos;

namespace Movi.Core.Domain.Interfaces;

public interface IMovieService
{
    Task<int> AddAsync(AddMovieDto dto);
}
