namespace Movi.Core.Domain.Dtos;

public class ReservationDto
{
    public string UserId { get; set; }
    public string ShowtimeId { get; set; }
    public List<string> SeatNumbers { get; set; }
}
