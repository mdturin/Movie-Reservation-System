using AutoMapper;
using Movi.Core.Application.Conditions;
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

    public async Task<List<MovieDto>> GetMoviesWithShowTimes(DateTime date)
    {
        var startTimeCondition = new FieldCondition<Showtime>(nameof(Showtime.StartTime), date);
        var showTimesCondition = new AnyCondition<Movie, Showtime>(nameof(Movie.Showtimes), startTimeCondition);
        var exp = showTimesCondition.ToExpression();
        var movies = await _context.GetMovies(exp);
        return _mapper.Map<List<MovieDto>>(movies);
    }

    public async Task<List<MovieDto>> GetMoviesWithShowTimes(IEnumerable<string> genres)
    {
        var movies = await _context
            .GetMoviesWithShowTimes(genres);
        return _mapper.Map<List<MovieDto>>(movies);
    }

    public async Task<List<MovieDto>> GetMovies()
    {
        var movies = await _context.GetMoviesAsync();
        return _mapper.Map<List<MovieDto>>(movies);
    }
}
