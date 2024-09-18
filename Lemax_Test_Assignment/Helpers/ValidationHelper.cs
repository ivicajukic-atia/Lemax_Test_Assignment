namespace Lemax_Test_Assignment.Helpers
{
  /// <summary>
  /// Provides utility methods for validating input values.
  /// </summary>
  public static class ValidationHelper
  {
    /// <summary>
    /// Validates that the provided value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="parameterName">The name of the parameter being validated, used in the exception message.</param>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public static void ValidateNotNull<T>(T value, string parameterName)
    {
      if (value == null)
      {
        throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
      }
    }

    /// <summary>
    /// Validates that the provided GUID is not an empty GUID.
    /// </summary>
    /// <param name="id">The GUID to check.</param>
    /// <param name="parameterName">The name of the parameter being validated, used in the exception message.</param>
    /// <exception cref="ArgumentException">Thrown when the GUID is empty.</exception>
    public static void ValidateGuid(Guid id, string parameterName)
    {
      if (id == Guid.Empty)
      {
        throw new ArgumentException($"{parameterName} cannot be an empty GUID.", parameterName);
      }
    }
  }
}
