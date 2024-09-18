using Lemax_Test_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lemax_Test_Assignment.Helpers
{
  /// <summary>
  /// Provides helper methods for creating standardized API responses.
  /// </summary>
  public static class ApiResponseHelper
  {
    /// <summary>
    /// Creates a successful response with the provided result object.
    /// </summary>
    /// <param name="result">The result object to include in the response.</param>
    /// <returns>An <see cref="ActionResult"/> representing a successful response.</returns>
    public static ActionResult CreateSuccessResponse(object result)
    {
      return new OkObjectResult(result);
    }

    /// <summary>
    /// Creates a bad request response with the provided error message.
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <returns>An <see cref="ActionResult"/> representing a bad request response.</returns>
    public static ActionResult CreateBadRequestResponse(string message)
    {
      return new BadRequestObjectResult(new ErrorResponse { ErrorMessage = message });
    }

    /// <summary>
    /// Creates a not found response with the provided error message.
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <returns>An <see cref="ActionResult"/> representing a not found response.</returns>
    public static ActionResult CreateNotFoundResponse(string message)
    {
      return new NotFoundObjectResult(new ErrorResponse { ErrorMessage = message });
    }

    /// <summary>
    /// Creates an error response with the provided error message and optional details.
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="details">Optional additional details about the error.</param>
    /// <returns>An <see cref="ActionResult"/> representing an error response.</returns>
    public static ActionResult CreateErrorResponse(string message, string details = null)
    {
      return new ObjectResult(new ErrorResponse { ErrorMessage = message, ErrorDetails = details })
      {
        StatusCode = StatusCodes.Status500InternalServerError
      };
    }
  }
}
