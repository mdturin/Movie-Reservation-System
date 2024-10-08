using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class SeatPublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<Seat>(context, manager)
{
    public override int Order => 5;
    public override string FileName => "JsonFiles\\Seats.json";
}
