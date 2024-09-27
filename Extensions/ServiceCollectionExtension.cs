using Asp.Versioning;

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
}
