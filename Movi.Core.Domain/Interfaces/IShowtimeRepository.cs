using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IShowtimeRepository : IBulkRepository
{
    Task<List<Showtime>> GetShowtimesAsync();
    Task<Showtime> GetShowtimeAsync(string id);
}
