using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class ActorPublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<Actor>(context, manager)
{
    public override string FileName => "JsonFiles\\Actors.json";
}
