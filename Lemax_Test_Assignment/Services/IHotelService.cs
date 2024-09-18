using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;


namespace Lemax_Test_Assignment.Services
{
  public interface IHotelService
  {
    Task<HotelDto> GetHotelByIdAsync(Guid id);
    Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
    Task<HotelDto> AddHotelAsync(HotelCreateDto hotelDto); 
    Task UpdateHotelAsync(HotelUpdateDto hotelDto);
    Task DeleteHotelAsync(Guid id);
    Task<IEnumerable<HotelDto>> SearchHotelsAsync(GeoLocationDto currentLocationDto, int pageNumber, int pageSize);
  }
}
