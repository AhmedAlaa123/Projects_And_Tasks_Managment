using Api.Extensions;
using Application.Exceptions;
using Application.Responses;

namespace Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = ex.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage
            });
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = 400,
                Message = "Validation failed",
                Errors = errors,
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var response = ex switch
        {
            NotFoundException => new ErrorResponse(StatusCodes.Status404NotFound, ex.Message),
            ValidationException => new ErrorResponse(StatusCodes.Status400BadRequest, ex.Message),
            UnauthorizedException => new ErrorResponse(StatusCodes.Status401Unauthorized, ex.Message),
            ForbiddenException => new ErrorResponse(StatusCodes.Status403Forbidden, ex.Message),
            InternalErrorException => new ErrorResponse(StatusCodes.Status500InternalServerError, ex.GetExceptionMessage()),
            FluentValidation.ValidationException fluentEx => new ErrorResponse(
            StatusCodes.Status400BadRequest,
            "Validation failed",
            fluentEx.Errors.Select(e => new ErrorResponse.ValidationError(e.PropertyName, e.ErrorMessage)).ToList()
    ),
            _ => new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        context.Response.StatusCode = response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
