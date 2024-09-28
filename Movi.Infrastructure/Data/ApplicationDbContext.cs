using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        builder.ApplyConfigurationsFromAssembly(assembly);

        assembly
            .GetTypes()
            .Where(t =>
            {
                return typeof(IDatabaseModel).IsAssignableFrom(t)
                    && !t.IsInterface && !t.IsAbstract;
            })
            .ToList()
            .ForEach(t => builder.Entity(t));

        base.OnModelCreating(builder);
    }
}
