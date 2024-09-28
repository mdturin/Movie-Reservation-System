using Movie_Reservation_System.Abstractions;

namespace Movie_Reservation_System.Models;

public class CinemaHall : ADatabaseModel
{
    public string Name { get; set; } // Name of the cinema hall
    public int TotalSeats { get; set; } // Total number of seats in the cinema hall
    public List<Showtime> Showtimes { get; set; } // List of showtimes in this cinema hall
}
