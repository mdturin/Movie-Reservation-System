using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Dtos;

public class AddMovieDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int DurationInMinutes { get; set; }
    public string Language { get; set; }
    public string Rating { get; set; }

    // Relationship properties
    public ICollection<Showtime> Showtimes { get; set; } = [];
    public ICollection<Actor> Cast { get; set; } = [];
    public string Director { get; set; }
    public string PosterUrl { get; set; }

    // Optional - Other fields
    public decimal TicketPrice { get; set; }
    public string TrailerUrl { get; set; }
}
