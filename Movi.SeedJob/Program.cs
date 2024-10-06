using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;
using Movi.Infrastructure.Repositories;
using Movi.SeedJob.Publishers;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();
var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

services.AddLogging(configure => configure.AddConsole());
services.AddTransient<IBulkRepository, BulkRepository>();
services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var serviceProvider = services.BuildServiceProvider();
await SeedDatabaseAsync(serviceProvider);

var userManager = serviceProvider
    .GetRequiredService<UserManager<ApplicationUser>>();
var context = serviceProvider
    .GetRequiredService<IBulkRepository>();

var publisherTypes = GetAllImplementations<IResourcePublisher>();
var resourceInstances = new List<IResourcePublisher>();
foreach (var type in publisherTypes)
{
    if (Activator.CreateInstance(type, context, userManager) is IResourcePublisher instance)
        resourceInstances.Add(instance);
}

var instances = resourceInstances.OrderBy(r => r.Order);
foreach (var instance in instances)
    await instance.PublishAsync();

static async Task SeedDatabaseAsync(IServiceProvider services)
{
    var context = services
        .GetRequiredService<ApplicationDbContext>();
    try
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
        await InitializeRoles(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

static async Task InitializeRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager
                .CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                Console.WriteLine($"Role '{roleName}' created successfully.");
            else
            {
                Console.WriteLine($"Error creating role '{roleName}':");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Role '{roleName}' already exist.");
        }
    }
}

static List<Type> GetAllImplementations<TBase>()
{
    // Get the assembly where the base class is defined (you can change it if needed)
    var assembly = Assembly.GetAssembly(typeof(TBase));

    // Filter the types that inherit from the base class and are not abstract
    var implementations = assembly
        .GetTypes()
        .Where(t => typeof(TBase).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass)
        .ToList();

    return implementations;
}
