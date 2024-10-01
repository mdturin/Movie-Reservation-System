using System.Reflection;
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
var userManager = serviceProvider
    .GetRequiredService<UserManager<ApplicationUser>>();
var context = serviceProvider
    .GetRequiredService<IBulkRepository>();

var publisherTypes = GetAllImplementations<IResourcePublisher>();
foreach (var type in publisherTypes)
{
    var instance = Activator
        .CreateInstance(type, context, userManager) as IResourcePublisher;
    await instance?.PublishAsync();
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
