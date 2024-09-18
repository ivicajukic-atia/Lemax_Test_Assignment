using Moq;
using AutoMapper;
using Lemax_Test_Assignment.Data.Interfaces;
using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;
using Lemax_Test_Assignment.Services;
using Microsoft.Extensions.Logging;
using Lemax_Test_Assignment.Tests.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class HotelServiceTests
{
  private readonly Mock<IHotelRepository> _mockHotelRepository;
  private readonly IMapper _mapper;
  private readonly HotelService _service;

  public HotelServiceTests()
  {
    // Initialize the mock repository and configure the in-memory data
    _mockHotelRepository = new Mock<IHotelRepository>();

    var testHotels = TestData.GetSampleHotels().ToList();
    _mockHotelRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                        .Returns<Guid>(id => Task.FromResult(testHotels.SingleOrDefault(h => h.Id == id)));

    // Configure AutoMapper
    _mapper = new MapperConfiguration(cfg =>
    {
      cfg.CreateMap<Hotel, HotelDto>();
      cfg.CreateMap<HotelCreateDto, Hotel>();
      cfg.CreateMap<HotelUpdateDto, Hotel>();
    }).CreateMapper();

    // Initialize the service with the mocked repository and logger
    _service = new HotelService(_mockHotelRepository.Object, _mapper, Mock.Of<ILogger<HotelService>>());
  }

  [Fact]
  public async Task GetHotelByIdAsync_ReturnsHotelDto_WhenHotelExists()
  {
    // Arrange
    // Create a hotel ID and a Hotel object for the test
    var hotelId = Guid.NewGuid();
    var hotel = new Hotel { Id = hotelId };
    var hotelDto = new HotelDto { Id = hotelId };

    // Setup the mock repository to return the Hotel object for the given ID
    _mockHotelRepository.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);

    // Act
    // Call the GetHotelByIdAsync method of the service
    var result = await _service.GetHotelByIdAsync(hotelId);

    // Assert
    // Verify that the result is the expected HotelDto
    Assert.Equal(hotelId, result.Id);
  }

  [Fact]
  public async Task GetHotelByIdAsync_ReturnsNull_WhenHotelDoesNotExist()
  {
    // Arrange
    // Create a hotel ID and set up the mock repository to return null
    var hotelId = Guid.NewGuid();
    _mockHotelRepository.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync((Hotel)null);

    // Act
    // Call the GetHotelByIdAsync method of the service
    var result = await _service.GetHotelByIdAsync(hotelId);

    // Assert
    // Verify that the result is null
    Assert.Null(result);
  }

  [Fact]
  public async Task AddHotelAsync_ThrowsArgumentNullException_WhenHotelDtoIsNull()
  {
    // Arrange
    // Prepare a null HotelCreateDto
    HotelCreateDto hotelDto = null;

    // Act & Assert
    // Verify that calling AddHotelAsync with a null HotelCreateDto throws an ArgumentNullException
    await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddHotelAsync(hotelDto));
  }

  [Fact]
  public async Task UpdateHotelAsync_ThrowsArgumentNullException_WhenHotelDtoIsNull()
  {
    // Arrange
    // Prepare a null HotelUpdateDto
    HotelUpdateDto hotelDto = null;

    // Act & Assert
    // Verify that calling UpdateHotelAsync with a null HotelUpdateDto throws an ArgumentNullException
    await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateHotelAsync(hotelDto));
  }

  [Fact]
  public async Task DeleteHotelAsync_DeletesHotel_WhenHotelExists()
  {
    // Arrange
    // Create a hotel ID and set up the mock repository to return a Hotel object
    var hotelId = Guid.NewGuid();
    _mockHotelRepository.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(new Hotel { Id = hotelId });
    _mockHotelRepository.Setup(repo => repo.DeleteAsync(hotelId)).Returns(Task.CompletedTask);

    // Act
    // Call the DeleteHotelAsync method of the service
    await _service.DeleteHotelAsync(hotelId);

    // Assert
    // Verify that DeleteAsync was called once with the correct hotel ID
    _mockHotelRepository.Verify(repo => repo.DeleteAsync(hotelId), Times.Once);
  }
}
