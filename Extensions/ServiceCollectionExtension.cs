using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie_Reservation_System.Data;
using System.Text;

namespace Movie_Reservation_System.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
         {
             options.ReportApiVersions = true;
             options.ApiVersionReader = new UrlSegmentApiVersionReader();
         }).AddApiExplorer(options =>
         {
             options.GroupNameFormat = "'v'VVV";
             options.SubstituteApiVersionInUrl = true;
         });

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config) 
    {
        return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            };
        });

        return services;
    }
}
