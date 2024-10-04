using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class CinemaHall : ADatabaseModel
{
    public string Name { get; set; }
    public int TotalSeats { get; set; }
    public ICollection<Showtime> Showtimes { get; set; }
}
