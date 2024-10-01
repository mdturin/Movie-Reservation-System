using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class Showtime : ADatabaseModel
{
    public DateTime StartTime { get; set; }
    public string CinemaHallId { get; set; }
    public CinemaHall CinemaHall { get; set; }
    public string MovieId { get; set; }
    public Movie Movie { get; set; }
}
