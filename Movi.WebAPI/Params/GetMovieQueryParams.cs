using System.Linq.Expressions;
using Movi.Core.Application.Conditions;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Entities;

namespace Movi.WebAPI.Params;

public class GetMovieQueryParams
{
    public string Genre { get; set; } = string.Empty;
    public DateTime ShowStartTime { get; set; } = DateTime.MinValue;

    public Expression<Func<Movie, bool>> ToExpression()
    {
        var result = new AndCondition<Movie>();
        AddGenreCondition(result);
        AddShowStartTimeCondition(result);
        return result.ToExpression();
    }

    private void AddShowStartTimeCondition(ACompositeCondition<Movie> result)
    {
        if (ShowStartTime == DateTime.MinValue)
        {
            return;
        }

        var startTimeCondition = new FieldCondition<Showtime>(nameof(Showtime.StartTime), ShowStartTime);
        var showTimesCondition = new AnyCondition<Movie, Showtime>(nameof(Movie.Showtimes), startTimeCondition);
        result.AddCondition(showTimesCondition);
    }

    private void AddGenreCondition(ACompositeCondition<Movie> result)
    {
        if (string.IsNullOrWhiteSpace(Genre)) return;

        var genres = Genre.Split(",");
        var genreCondition = new AnyContainsCondition<Movie>(nameof(Movie.Genre), genres, true);
        result.AddCondition(genreCondition);
    }
}
