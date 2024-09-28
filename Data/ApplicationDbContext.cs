using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Interfaces;

namespace Movie_Reservation_System.Data;

public class ApplicationUser : IdentityUser { }

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)

    {
    }

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
