using Lemax_Test_Assignment.Models;

namespace Lemax_Test_Assignment.DTOs
{
  public class HotelDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public GeoLocationDto Location { get; set; }
    public double Distance { get; set; } // Only used in search results
  }
}
