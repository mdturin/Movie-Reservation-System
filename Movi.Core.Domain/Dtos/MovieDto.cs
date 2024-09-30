namespace Movi.Core.Domain.Dtos;

public class MovieDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int DurationInMinutes { get; set; }
    public string Language { get; set; }
    public string Rating { get; set; }
    public string Director { get; set; }
    public string PosterUrl { get; set; }
    public decimal TicketPrice { get; set; }
    public string TrailerUrl { get; set; }
    public ICollection<ActorDto> Cast { get; set; } = [];
    public ICollection<ShowtimeDto> Showtimes { get; set; } = [];
}
