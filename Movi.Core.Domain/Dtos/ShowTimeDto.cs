namespace Movi.Core.Domain.Dtos;

public class ShowtimeDto
{
    public string Id { get; set; }
    public DateTime StartTime { get; set; }
    public string CinemaHallId { get; set; }
    public string MovieId { get; set; }
}
