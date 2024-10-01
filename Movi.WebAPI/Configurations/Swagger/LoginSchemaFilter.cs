using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Movi.Core.Domain.Dtos;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Movi.WebAPI.Configurations.Swagger;

public class LoginSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!(context.Type == typeof(UserLoginDto)))
            return;

        schema.Example = new OpenApiObject
        {
            ["username"] = new OpenApiString("root-user"),
            ["password"] = new OpenApiString("Root@123")
        };
    }
}
