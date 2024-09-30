namespace Movi.Core.Domain.Dtos;

public class CinemaHallDto
{
    public string Name { get; set; } // Name of the cinema hall
    public int TotalSeats { get; set; } // Total number of seats in the cinema hall
    public ICollection<ShowTimeDto> Showtimes { get; set; } = [];
}
