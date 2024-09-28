using AutoMapper;
using Movie_Reservation_System.Dtos;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;

namespace Movie_Reservation_System.Services;

public interface IMovieService
{
    Task<int> AddAsync(AddMovieDto dto);
}

public class MovieService(IMapper mapper, IBulkRepository context) : IMovieService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBulkRepository _context = context;

    public async Task<int> AddAsync(AddMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return await _context.AddAsync(movie);
    }
}
