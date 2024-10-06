using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface ISeatService
{
    Task<List<Seat>> GetAvailableSeatsAsync(IEnumerable<string> seatIds);
}
