namespace Movi.Core.Domain.Dtos;

public class ReservationDto
{
    public string UserId { get; set; }
    public List<string> SeatIds { get; set; }
}
