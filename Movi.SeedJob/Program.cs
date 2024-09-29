using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;
using Movi.Infrastructure.Repositories;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();
var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

services.AddLogging(configure => configure.AddConsole());
services.AddTransient<IBulkRepository, BulkRepository>();
services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var serviceProvider = services.BuildServiceProvider();
var userManager = serviceProvider
    .GetRequiredService<UserManager<ApplicationUser>>();

if (userManager.FindByEmailAsync("root@gmail.com") != null)
    Console.WriteLine("found root.");
