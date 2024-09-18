using AutoMapper;
using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;

namespace Lemax_Test_Assignment.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {    
      CreateMap<Hotel, HotelDto>();
      CreateMap<HotelCreateDto, Hotel>();
      CreateMap<HotelUpdateDto, Hotel>();
      CreateMap<GeoLocationDto, GeoLocation>();
      CreateMap<GeoLocation, GeoLocationDto>();
    }
  }
}
