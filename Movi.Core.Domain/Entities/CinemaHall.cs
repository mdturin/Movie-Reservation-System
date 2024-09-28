using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class CinemaHall : ADatabaseModel
{
    public string Name { get; set; } // Name of the cinema hall
    public int TotalSeats { get; set; } // Total number of seats in the cinema hall
    public ICollection<Showtime> Showtimes { get; set; } // List of showtimes in this cinema hall
}
