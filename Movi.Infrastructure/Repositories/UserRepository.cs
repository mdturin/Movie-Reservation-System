using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Infrastructure.Repositories;

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
