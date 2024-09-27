using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movie_Reservation_System.Configurations;
using Movie_Reservation_System.Data;
using Movie_Reservation_System.Extensions;
using Movie_Reservation_System.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioning();
builder.Services.AddDatabase(config);
builder.Services.AddIdentity();
builder.Services.AddAuthentication();

builder.Services
    .AddSingleton<JwtService>()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerApp();
    app.UseDeveloperExceptionPage();
}

await SeedDatabase(app);

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
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

static async Task SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services
            .GetRequiredService<ApplicationDbContext>();
        try
        {
            await context.Database.MigrateAsync();
            await InitializeRoles(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
}