using AutoMapper;
using Movie_Reservation_System.Dtos;
using Movie_Reservation_System.Interfaces;
using Movie_Reservation_System.Models;

namespace Movie_Reservation_System.Services;

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
