using Lemax_Test_Assignment.Models;

namespace Lemax_Test_Assignment.Middleware
{
  public class GlobalExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
        _logger.LogError(ex, "Unhandled exception occurred.");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var errorResponse = new ErrorResponse
        {
          ErrorMessage = "An unexpected error occurred. Please try again later.",
          ErrorDetails = ex.Message
        };
        await context.Response.WriteAsJsonAsync(errorResponse);
      }
    }
  }
}
