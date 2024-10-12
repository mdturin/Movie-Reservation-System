using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface ISeatRepository : IBulkRepository
{
    Task<List<Seat>> GetAvailableSeatsAsync();
    Task<List<Seat>> GetAvailableSeatsAsync(IEnumerable<string> seatNumbers);
}
