using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class ShowtimePublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<Showtime>(context, manager)
{
    public override string FileName => "JsonFiles\\Showtimes.json";
}
