namespace Lemax_Test_Assignment.Models
{
  public class Hotel
  {
    public Guid Id { get; set; } 
    public string Name { get; set; } 
    public decimal Price { get; set; } // Price per night
    public GeoLocation Location { get; set; } // Geographical location of the hotel
  }
}
