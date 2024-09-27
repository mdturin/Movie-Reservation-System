namespace Movie_Reservation_System.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication UseSwaggerApp(this WebApplication app)
    {
        app.UseSwagger().UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
