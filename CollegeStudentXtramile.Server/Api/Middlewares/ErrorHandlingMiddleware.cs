using CollegeStudentXtramile.Server.Api.Resources.Responses;
using CollegeStudentXtramile.Server.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace CollegeStudentXtramile.Server.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponseJSON(
            TraceId: context.TraceIdentifier,
            Title: string.Empty,
            Errors: new Dictionary<string, List<string>>()
        );


        switch (exception)
        {
            case DbUpdateException dbUpdateEx when dbUpdateEx.IsDuplicateKeyException():
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Title = "Duplicate key error";
                errorResponse.Errors.Add("Id", new List<string> { "A student with the same ID already exists" });
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Title = "Internal Server Error";
                // Do not specify the error details for internal server errors
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(result);
    }

}
