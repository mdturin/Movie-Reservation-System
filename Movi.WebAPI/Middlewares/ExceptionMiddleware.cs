using System.Net;
using System.Text.Json;
using Movi.Core.Domain.Dtos;

namespace Movi.WebAPI.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            _logger.LogError($"Something went wrong: {ex.Message}", ex);

            // Handle the exception and return a formatted response
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Customize the error response message
        var errorResponse = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = "An unexpected error occurred. Please try again later.",
            Details = exception.Message // Optional: remove or change in production
        };

        // Serialize the error response to JSON and return it
        var errorJson = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(errorJson);
    }
}
