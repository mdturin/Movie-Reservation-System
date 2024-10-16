using System.Linq.Expressions;
using AutoMapper;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Services;

public class MovieService(IMapper mapper, IBulkRepository context)
    : IMovieService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBulkRepository _context = context;

    public Task AddAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return _context.AddAsync(movie);
    }

    public Task UpdateAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return _context.UpdateAsync(movie);
    }

    public Task DeleteAsync(string id)
    {
        return _context.DeleteAsync<Movie>(id);
    }

    public async Task<List<MovieDto>> GetMoviesAsync(Expression<Func<Movie, bool>> exp)
    {
        var movies = await _context
            .GetItemsAsync(exp, (m) => m.Showtimes, (m) => m.Cast);
        return _mapper.Map<List<MovieDto>>(movies);
    }
}
