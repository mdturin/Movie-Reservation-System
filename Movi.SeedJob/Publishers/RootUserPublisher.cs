using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class RootUserPublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<ApplicationUser>(context, manager)
{
    public override string FileName => "JsonFiles\\RootUsers.json";

    public override async Task<object> PublishAsync()
    {
        await CleanupDatabaseAsync();
        var users = Deserialize() as List<ApplicationUser>;
        foreach (var user in users)
        {
            await _manager.CreateAsync(user, "Root@123");
            if (!await _manager.IsInRoleAsync(user, "Admin"))
                await _manager.AddToRoleAsync(user, "Admin");
        }

        return users;
    }
}


