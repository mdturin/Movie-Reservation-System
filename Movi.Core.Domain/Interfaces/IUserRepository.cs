using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser> ValidateUserAsync(string username, string password);
    Task<ApplicationUser> GetUserByUsernameAsync(string username);
}
