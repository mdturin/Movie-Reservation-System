namespace Movi.WebAPI.Params;

public class GetMovieQueryParams
{
    public string Genre { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.MinValue;
}
