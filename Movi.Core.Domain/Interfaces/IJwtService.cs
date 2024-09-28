using Movi.Core.Domain.Entities;

namespace Movi.Core.Domain.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}
