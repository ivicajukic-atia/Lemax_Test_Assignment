using Lemax_Test_Assignment.Models;

namespace Lemax_Test_Assignment.Helpers
{
  /// <summary>
  /// Provides utility methods for geographical calculations.
  /// </summary>
  public static class GeoLocationHelper
  {
    /// <summary>
    /// Calculates the great-circle distance between two geographical locations. using Haversine formula
    /// </summary>
    /// <param name="location1">The first geographical location.</param>
    /// <param name="location2">The second geographical location.</param>
    /// <returns>The distance in kilometers between the two locations.</returns>
    public static double CalculateDistance(GeoLocation location1, GeoLocation location2)
    {
      const double EarthRadiusKm = 6371;

      double dLat = DegreesToRadians(location2.Latitude - location1.Latitude);
      double dLon = DegreesToRadians(location2.Longitude - location1.Longitude);

      double lat1 = DegreesToRadians(location1.Latitude);
      double lat2 = DegreesToRadians(location2.Latitude);

      double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                 Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
      double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

      return EarthRadiusKm * c;
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>The angle in radians.</returns>
    private static double DegreesToRadians(double degrees)
    {
      return degrees * (Math.PI / 180);
    }
  }
}
