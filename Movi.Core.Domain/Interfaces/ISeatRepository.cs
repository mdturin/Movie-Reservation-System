using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface ISeatRepository : IBulkRepository
{
    Task<List<Seat>> GetAvailableSeatsByShowTimeIdAsync(string showTimeId);
    Task<List<Seat>> GetAvailableSeatsAsync(string showtimeId, IEnumerable<string> seatNumbers);
}
