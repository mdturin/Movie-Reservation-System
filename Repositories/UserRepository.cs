using Microsoft.AspNetCore.Identity;
using Movie_Reservation_System.Data;

namespace Movie_Reservation_System.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> ValidateUserAsync(string username, string password);
    Task<ApplicationUser> GetUserByUsernameAsync(string username);
}

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> ValidateUserAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            return user;
        }

        return null; // Invalid user or password
    }

    public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }
}
