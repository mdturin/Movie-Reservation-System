namespace Movi.WebAPI.Params;

public class GetMovieQueryParams
{
    public string Genre { get; set; } = string.Empty;
    public DateTime ShowStartTime { get; set; } = DateTime.MinValue;
}
