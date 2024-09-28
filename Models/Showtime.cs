using Movie_Reservation_System.Abstractions;

namespace Movie_Reservation_System.Models;

public class Showtime : ADatabaseModel
{
    public DateTime StartTime { get; set; } // The start time of the movie showing
    public int CinemaHallId { get; set; } // The cinema hall where the movie is shown
    public CinemaHall CinemaHall { get; set; } // Navigation property to the cinema hall
    public int MovieId { get; set; } // The movie being shown
    public Movie Movie { get; set; } // Navigation property to the movie
}
