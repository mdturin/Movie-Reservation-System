using Movie_Reservation_System.Abstractions;

namespace Movie_Reservation_System.Models;

public class Movie : ADatabaseModel
{
    public string Title { get; set; } // Movie title
    public string Description { get; set; } // Brief summary or description
    public string Genre { get; set; } // Genre(s) of the movie (e.g., Action, Drama)
    public DateTime ReleaseDate { get; set; } // Release date of the movie
    public int DurationInMinutes { get; set; } // Duration of the movie in minutes
    public string Language { get; set; } // Language of the movie
    public string Rating { get; set; } // Movie rating (e.g., PG-13, R)

    // Relationship properties
    public List<Showtime> Showtimes { get; set; } // List of showtimes for this movie
    public List<Actor> Cast { get; set; } // List of main actors in the movie
    public string Director { get; set; } // Name of the movie director
    public string PosterUrl { get; set; } // URL for the movie poster image

    // Optional - Other fields
    public decimal TicketPrice { get; set; } // Price per ticket for this movie
    public string TrailerUrl { get; set; } // URL for the movie trailer video
}
