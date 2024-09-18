namespace Lemax_Test_Assignment.DTOs
{
  public class HotelUpdateDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public GeoLocationDto Location { get; set; }
  }
}
