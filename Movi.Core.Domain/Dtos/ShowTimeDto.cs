namespace Movi.Core.Domain.Dtos;

public class ShowTimeDto
{
    public DateTime StartTime { get; set; } // The start time of the movie showing
    public string CinemaHallId { get; set; } // The cinema hall where the movie is shown
    public string MovieId { get; set; } // The movie being shown
}
