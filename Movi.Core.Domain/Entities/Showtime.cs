using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class Showtime : ADatabaseModel
{
    public DateTime StartTime { get; set; } // The start time of the movie showing
    public string CinemaHallId { get; set; } // The cinema hall where the movie is shown
    public CinemaHall CinemaHall { get; set; } // Navigation property to the cinema hall
    public string MovieId { get; set; } // The movie being shown
    public Movie Movie { get; set; } // Navigation property to the movie
}
