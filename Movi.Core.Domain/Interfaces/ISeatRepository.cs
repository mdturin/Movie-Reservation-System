using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface ISeatRepository : IBulkRepository
{
    Task<List<Seat>> GetAvailableSeatsByShowTimeIdAsync(string showTimeId);
    Task<List<Seat>> GetAvailableSeatsAsync(string showtimeId, IEnumerable<string> seatNumbers);
    Task<List<Seat>> GetUnavailableSeatsAsync(string showtimeId, IEnumerable<string> seatNumbers);
}
