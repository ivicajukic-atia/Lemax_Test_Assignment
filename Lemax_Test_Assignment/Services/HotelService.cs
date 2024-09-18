using AutoMapper;
using Lemax_Test_Assignment.Data.Interfaces;
using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Helpers;
using Lemax_Test_Assignment.Models;
using Microsoft.Extensions.Logging;

namespace Lemax_Test_Assignment.Services
{
  // The HotelService class handles hotel-related operations, 
  public class HotelService : IHotelService
  {
    // Dependencies injected via constructor
    private readonly IHotelRepository _hotelRepository; // Repository for data access
    private readonly IMapper _mapper; // Mapper for DTO to entity conversions
    private readonly ILogger<HotelService> _logger; // Logger for logging service operations

    // Constructor with dependency injection
    public HotelService(IHotelRepository hotelRepository, IMapper mapper, ILogger<HotelService> logger)
    {
      _hotelRepository = hotelRepository;
      _mapper = mapper;
      _logger = logger;
    }

    // Retrieves a hotel by its ID
    public async Task<HotelDto> GetHotelByIdAsync(Guid id)
    {
      _logger.LogInformation("Request to get hotel with ID {Id}", id);

      try
      {
        // Validate ID parameter
        ValidationHelper.ValidateGuid(id, nameof(id));

        // Fetch hotel from repository
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null)
        {
          _logger.LogWarning("Hotel with ID {Id} not found", id);
          return null; // Return null if not found
        }

        _logger.LogInformation("Hotel with ID {Id} retrieved successfully", id);
        // Map hotel entity to DTO and return
        return _mapper.Map<HotelDto>(hotel);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while getting hotel with ID {Id}", id);
        throw; // Rethrow exception for further handling 
      }
    }

    // Retrieves all hotels
    public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
    {
      _logger.LogInformation("Request to get all hotels");

      try
      {
        // Fetch all hotels from repository
        var hotels = await _hotelRepository.GetAllAsync();
        _logger.LogInformation("Retrieved {Count} hotels", hotels.Count());

        // Map all hotel entities to DTOs and return
        return _mapper.Map<IEnumerable<HotelDto>>(hotels);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while getting all hotels");
        throw; // Rethrow exception for further handling
      }
    }

    // Adds a new hotel
    public async Task<HotelDto> AddHotelAsync(HotelCreateDto hotelDto)
    {
      if (hotelDto == null)
      {
        _logger.LogWarning("AddHotelAsync request body is null");
        throw new ArgumentNullException(nameof(hotelDto), "Hotel data must be provided.");
      }

      _logger.LogInformation("Request to add hotel with Name {Name}", hotelDto.Name);

      try
      {
        // Map DTO to hotel entity
        var hotel = _mapper.Map<Hotel>(hotelDto);
        // Add hotel to repository
        await _hotelRepository.AddAsync(hotel);

        _logger.LogInformation("Hotel with ID {Id} added successfully", hotel.Id);
        // Map newly added hotel entity to DTO and return
        return _mapper.Map<HotelDto>(hotel);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while adding hotel with Name {Name}", hotelDto.Name);
        throw; // Rethrow exception for further handling
      }
    }

    // Updates an existing hotel
    public async Task UpdateHotelAsync(HotelUpdateDto hotelDto)
    {
      if (hotelDto == null)
      {
        _logger.LogWarning("UpdateHotelAsync request body is null");
        throw new ArgumentNullException(nameof(hotelDto), "Hotel data must be provided.");
      }

      _logger.LogInformation("Request to update hotel with ID {Id}", hotelDto.Id);

      try
      {
        // Map DTO to hotel entity
        var hotel = _mapper.Map<Hotel>(hotelDto);
        // Update hotel in repository
        await _hotelRepository.UpdateAsync(hotel);
        _logger.LogInformation("Hotel with ID {Id} updated successfully", hotelDto.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while updating hotel with ID {Id}", hotelDto.Id);
        throw; // Rethrow exception for further handling
      }
    }

    // Deletes a hotel by its ID
    public async Task DeleteHotelAsync(Guid id)
    {
      _logger.LogInformation("Request to delete hotel with ID {Id}", id);

      try
      {
        // Validate ID parameter
        ValidationHelper.ValidateGuid(id, nameof(id));
        // Delete hotel from repository
        await _hotelRepository.DeleteAsync(id);
        _logger.LogInformation("Hotel with ID {Id} deleted successfully", id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while deleting hotel with ID {Id}", id);
        throw; // Rethrow exception for further handling
      }
    }

    // Searches hotels based on current location and pagination
    public async Task<IEnumerable<HotelDto>> SearchHotelsAsync(GeoLocationDto currentLocationDto, int pageNumber = 1, int pageSize = 10)
    {
      if (currentLocationDto == null)
      {
        _logger.LogWarning("SearchHotelsAsync request data is null");
        throw new ArgumentNullException(nameof(currentLocationDto), "GeoLocation data must be provided.");
      }

      _logger.LogInformation("Request to search hotels near location {Location}", currentLocationDto);

      try
      {
        // Map DTO to location entity
        var currentLocation = _mapper.Map<GeoLocation>(currentLocationDto);
        // Fetch all hotels from repository
        var hotels = await _hotelRepository.GetAllAsync();

        // Calculate distance from current location to each hotel
        var hotelsWithDistance = hotels.Select(h => new
        {
          Hotel = h,
          Distance = GeoLocationHelper.CalculateDistance(currentLocation, h.Location)
        });

        // Sort hotels by distance and price, and apply pagination
        var sortedHotels = hotelsWithDistance
            .OrderBy(h => h.Distance)
            .ThenBy(h => h.Hotel.Price)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(h => _mapper.Map<HotelDto>(h.Hotel))
            .ToList();

        // Assign the calculated distance to each mapped HotelDto
        for (int i = 0; i < sortedHotels.Count; i++)
        {
          sortedHotels[i].Distance = hotelsWithDistance.ElementAt((pageNumber - 1) * pageSize + i).Distance;
        }

        _logger.LogInformation("Search completed with {Count} hotels found", sortedHotels.Count);
        return sortedHotels;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while searching hotels near location {Location}", currentLocationDto);
        throw; // Rethrow exception for further handling
      }
    }
  }
}
