using Movie_Reservation_System.Abstractions;

namespace Movie_Reservation_System.Models;

public class Actor : ADatabaseModel
{
    public string Name { get; set; } // Actor's name
    public string Role { get; set; } // Actor's role in the movie
    public string PhotoUrl { get; set; } // Actor's photo (optional)
}
