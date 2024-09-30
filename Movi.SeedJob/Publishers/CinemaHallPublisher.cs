using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class CinemaHallPublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<CinemaHall>(context, manager)
{
    public override string FileName => "JsonFiles\\CinemaHalls.json";
}
