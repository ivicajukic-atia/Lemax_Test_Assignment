using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Lemax_Test_Assignment.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HotelController : ControllerBase
{
  private readonly IHotelService _hotelService;
  private readonly ILogger<HotelController> _logger;

  public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
  {
    _hotelService = hotelService;
    _logger = logger;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<HotelDto>> GetHotel(Guid id)
  {
    _logger.LogInformation("Request to get hotel with ID {Id}", id);

    try
    {
      ValidationHelper.ValidateGuid(id, nameof(id));

      var hotel = await _hotelService.GetHotelByIdAsync(id);
      if (hotel == null)
      {
        _logger.LogWarning("Hotel with ID {Id} not found", id);
        return ApiResponseHelper.CreateNotFoundResponse($"Hotel with ID {id} not found");
      }
      return Ok(hotel); // Return 200 OK with the result
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred while getting hotel with ID {Id}", id);
      return ApiResponseHelper.CreateErrorResponse("An error occurred while retrieving the hotel.", ex.Message);
    }
  }

  [HttpPost]
  public async Task<ActionResult> CreateHotel([FromBody] HotelCreateDto hotelDto)
  {
    if (hotelDto == null)
    {
      _logger.LogWarning("CreateHotel request body is null");
      return ApiResponseHelper.CreateErrorResponse("Hotel data must be provided.");
    }

    _logger.LogInformation("Creating hotel with Name {Name}", hotelDto.Name);
    try
    {
      var createdHotel = await _hotelService.AddHotelAsync(hotelDto);
      return CreatedAtAction(nameof(GetHotel), new { id = createdHotel.Id }, createdHotel);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred while creating hotel with Name {Name}", hotelDto.Name);
      return ApiResponseHelper.CreateErrorResponse("An error occurred while creating the hotel.", ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelUpdateDto hotelDto)
  {
    if (hotelDto == null)
    {
      _logger.LogWarning("UpdateHotel request body is null");
      return ApiResponseHelper.CreateErrorResponse("Hotel data must be provided.");
    }

    if (id != hotelDto.Id)
    {
      _logger.LogWarning("Mismatch between URL ID {Id} and body ID {BodyId}", id, hotelDto.Id);
      return ApiResponseHelper.CreateErrorResponse("ID in URL and body must match.");
    }

    try
    {
      await _hotelService.UpdateHotelAsync(hotelDto);
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred while updating hotel with ID {Id}", id);
      return ApiResponseHelper.CreateErrorResponse("An error occurred while updating the hotel.", ex.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteHotel(Guid id)
  {
    try
    {
      ValidationHelper.ValidateGuid(id, nameof(id));
      await _hotelService.DeleteHotelAsync(id);
      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred while deleting hotel with ID {Id}", id);
      return ApiResponseHelper.CreateErrorResponse("An error occurred while deleting the hotel.", ex.Message);
    }
  }

  [HttpGet("search")]
  public async Task<ActionResult<IEnumerable<HotelDto>>> SearchHotels([FromQuery] GeoLocationDto currentLocation, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
  {
    if (currentLocation == null)
    {
      _logger.LogWarning("SearchHotels query parameter is null");
      return ApiResponseHelper.CreateErrorResponse("GeoLocation data must be provided.");
    }

    try
    {
      var hotels = await _hotelService.SearchHotelsAsync(currentLocation, pageNumber, pageSize);
      return Ok(hotels);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred while searching hotels near location {Location}", currentLocation);
      return ApiResponseHelper.CreateErrorResponse("An error occurred while searching for hotels.", ex.Message);
    }
  }
}
