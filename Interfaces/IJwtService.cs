using Movie_Reservation_System.Data;

namespace Movie_Reservation_System.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}
