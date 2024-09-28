using Movie_Reservation_System.Data;

namespace Movie_Reservation_System.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser> ValidateUserAsync(string username, string password);
    Task<ApplicationUser> GetUserByUsernameAsync(string username);
}
