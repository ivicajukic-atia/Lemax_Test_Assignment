namespace Lemax_Test_Assignment.DTOs
{
  public class HotelCreateDto
  {
    public Guid Id { get; set; } = Guid.NewGuid(); // 
    public string Name { get; set; }
    public decimal Price { get; set; }
    public GeoLocationDto Location { get; set; }
  }
}
