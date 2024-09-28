using AutoMapper;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Domain.Services;

public class MovieService(IMapper mapper, IBulkRepository context)
    : IMovieService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBulkRepository _context = context;

    public async Task<int> AddAsync(AddMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return await _context.AddAsync(movie);
    }
}
