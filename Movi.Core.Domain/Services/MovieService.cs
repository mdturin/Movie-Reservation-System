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

    public async Task<int> AddAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return await _context.AddAsync(movie);
    }

    public Task<int> UpdateAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return _context.UpdateAsync(movie);
    }

    public async Task DeleteAsync(string id)
    {
        await _context.DeleteAsync<Movie>(id);
    }
}
