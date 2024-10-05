using System.Linq.Expressions;
using AutoMapper;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Services;

public class MovieService(IMapper mapper, IMovieRepository context)
    : IMovieService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMovieRepository _context = context;

    public Task<int> AddAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        return _context.AddAsync(movie);
    }

    public Task<int> UpdateAsync(MovieDto dto)
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
        var movies = await _context.GetMoviesAsync(exp);
        return _mapper.Map<List<MovieDto>>(movies);
    }
}
