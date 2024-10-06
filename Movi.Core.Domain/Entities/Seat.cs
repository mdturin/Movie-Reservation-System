using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class Seat : ADatabaseModel
{
    public string SeatNumber { get; set; }
    public bool IsAvailable { get; set; }
    public string ShowtimeId { get; set; }
    public Showtime Showtime { get; set; }
}
