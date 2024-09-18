using Microsoft.Extensions.Logging;
using Lemax_Test_Assignment.Services;
using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class HotelControllerTests
{
  private readonly Mock<IHotelService> _mockHotelService;
  private readonly Mock<ILogger<HotelController>> _mockLogger;
  private readonly HotelController _controller;

  public HotelControllerTests()
  {
    // Initialize the mocks and the controller
    _mockHotelService = new Mock<IHotelService>();
    _mockLogger = new Mock<ILogger<HotelController>>();
    _controller = new HotelController(_mockHotelService.Object, _mockLogger.Object);
  }

  [Fact]
  public async Task GetHotel_ReturnsHotelDto_WhenHotelExists()
  {
    // Arrange
    // Create a hotel ID and a HotelDto object for the test
    var hotelId = Guid.NewGuid();
    var hotelDto = new HotelDto { Id = hotelId };

    // Setup the mock service to return the HotelDto when GetHotelByIdAsync is called
    _mockHotelService.Setup(service => service.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotelDto);

    // Act
    // Call the GetHotel method of the controller
    var result = await _controller.GetHotel(hotelId) as ActionResult<HotelDto>;

    // Assert
    // Verify that the result is an OkObjectResult and contains the expected HotelDto
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var returnValue = Assert.IsType<HotelDto>(okResult.Value);
    Assert.Equal(hotelId, returnValue.Id);
  }

  [Fact]
  public async Task GetHotel_ReturnsNotFound_WhenHotelDoesNotExist()
  {
    // Arrange
    // Create a hotel ID and set up the mock service to return null for this ID
    var hotelId = Guid.NewGuid();
    _mockHotelService.Setup(service => service.GetHotelByIdAsync(hotelId)).ReturnsAsync((HotelDto)null);

    // Act
    // Call the GetHotel method of the controller
    var result = await _controller.GetHotel(hotelId) as ActionResult<HotelDto>;

    // Assert
    // Verify that the result is a NotFoundObjectResult
    var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

    // Verify that the error message is as expected
    var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
    Assert.Equal($"Hotel with ID {hotelId} not found", errorResponse.ErrorMessage);
  }

  [Fact]
  public async Task CreateHotel_ReturnsCreatedAtActionResult_WhenHotelIsCreated()
  {
    // Arrange
    // Create a HotelCreateDto object and a HotelDto object representing the created hotel
    var hotelDto = new HotelCreateDto { Name = "New Hotel" };
    var createdHotel = new HotelDto { Id = Guid.NewGuid(), Name = hotelDto.Name };

    // Set up the mock service to return the createdHotel
    _mockHotelService.Setup(service => service.AddHotelAsync(hotelDto)).ReturnsAsync(createdHotel);

    // Act
    // Call the CreateHotel method of the controller
    var result = await _controller.CreateHotel(hotelDto) as ActionResult;

    // Assert
    // Verify that the result is a CreatedAtActionResult and contains the expected HotelDto
    var createdResult = Assert.IsType<CreatedAtActionResult>(result);
    var returnValue = Assert.IsType<HotelDto>(createdResult.Value);
    Assert.Equal(createdHotel.Id, returnValue.Id);
  }

  [Fact]
  public async Task UpdateHotel_ReturnsBadRequest_WhenIdsDoNotMatch()
  {
    // Arrange
    // Create a mismatched HotelUpdateDto with a different ID than the one in the URL
    var hotelId = Guid.NewGuid();
    var hotelDto = new HotelUpdateDto { Id = Guid.NewGuid() }; // Mismatched ID

    // Act
    // Call the UpdateHotel method of the controller
    var result = await _controller.UpdateHotel(hotelId, hotelDto) as IActionResult;

    // Assert
    // Verify that the result is a BadRequestObjectResult
    var badRequestResult = Assert.IsType<ObjectResult>(result);
    var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
    Assert.Equal("ID in URL and body must match.", errorResponse.ErrorMessage);
  }

  [Fact]
  public async Task DeleteHotel_ReturnsNoContent_WhenHotelIsDeleted()
  {
    // Arrange
    // Create a hotel ID and set up the mock service to complete the delete operation
    var hotelId = Guid.NewGuid();
    _mockHotelService.Setup(service => service.DeleteHotelAsync(hotelId)).Returns(Task.CompletedTask);

    // Act
    // Call the DeleteHotel method of the controller
    var result = await _controller.DeleteHotel(hotelId) as IActionResult;

    // Assert
    // Verify that the result is a NoContentResult
    var noContentResult = Assert.IsType<NoContentResult>(result);
    Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
  }

  [Fact]
  public async Task DeleteHotel_ReturnsNotFound_WhenHotelDoesNotExist()
  {
    // Arrange
    // Create a hotel ID and set up the mock service to throw an exception when trying to delete
    var hotelId = Guid.NewGuid();
    _mockHotelService.Setup(service => service.DeleteHotelAsync(hotelId)).Throws(new KeyNotFoundException());

    // Act
    // Call the DeleteHotel method of the controller
    var result = await _controller.DeleteHotel(hotelId) as IActionResult;

    // Assert
    // Verify that the result is a NotFoundObjectResult
    var notFoundResult = Assert.IsType<ObjectResult>(result);
    var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
    Assert.Equal($"An error occurred while deleting the hotel.", errorResponse.ErrorMessage);
  }
}
