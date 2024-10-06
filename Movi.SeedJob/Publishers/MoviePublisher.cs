using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public class MoviePublisher(IBulkRepository context, UserManager<ApplicationUser> manager)
    : AResourcePublisher<Movie>(context, manager)
{
    public override int Order => 1;
    public override string FileName => "JsonFiles\\Movies.json";
}
