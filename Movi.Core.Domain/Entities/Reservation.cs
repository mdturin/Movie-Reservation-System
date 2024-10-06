using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class Reservation : ADatabaseModel
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string SeatId { get; set; }
    public Seat Seat { get; set; }
    public string ShowtimeId { get; set; }
    public Showtime Showtime { get; set; }
    public DateTime ReservedAt { get; set; }
}
