using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;
using Movi.Core.Domain.Services;
using Movi.Infrastructure.Data;
using Movi.Infrastructure.Repositories;
using Movi.Infrastructure.Security;
using Movi.WebAPI.Configurations;
using Movi.WebAPI.Extensions;
using Movi.WebAPI.Middlewares;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerApp();
builder.Services.AddVersioning();
builder.Services.AddDatabase(config);
builder.Services.AddIdentity();
builder.Services.AddAuthenticationService(config);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services
    .AddScoped<IJwtService, JwtService>()
    .AddScoped<IMovieService, MovieService>()
    .AddScoped<IBulkRepository, BulkRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerApp();
    app.UseDeveloperExceptionPage();
}

await SeedDatabase(app);

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

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

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services
        .GetRequiredService<ApplicationDbContext>();
    try
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
        await InitializeRoles(services);
        await InitializeRootAdmin(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

static async Task InitializeRootAdmin(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    var rootUser = new ApplicationUser()
    {
        Email = "root@gmail.com",
        UserName = "root-user",
        EmailConfirmed = true,
    };

    if (await userManager.FindByEmailAsync(rootUser.Email) != null)
    {
        await userManager.DeleteAsync(rootUser);
    }

    await userManager.CreateAsync(rootUser, "Root@123");
    if (!await userManager.IsInRoleAsync(rootUser, "Admin"))
    {
        var result = await userManager
            .AddToRoleAsync(rootUser, "Admin");
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                Console.WriteLine($"Error adding root user to Admin role: {error.Description}");
        }
    }
}
