using Lemax_Test_Assignment.Data.Interfaces;
using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemax_Test_Assignment.Data.Repositories
{
  // InMemoryHotelRepository is a simple in-memory implementation of the IHotelRepository interface.
  // It is useful for testing this assignment where no persistent storage is required.
  public class InMemoryHotelRepository : IHotelRepository
  {
    // List to store hotels in memory
    private readonly List<Hotel> _hotels = new List<Hotel>();

    // Retrieves a hotel by its ID
    public async Task<Hotel> GetByIdAsync(Guid id)
    {
      // Simulate asynchronous operation
      return await Task.FromResult(_hotels.FirstOrDefault(h => h.Id == id));
    }

    // Retrieves all hotels
    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
      // Simulate asynchronous operation
      return await Task.FromResult(_hotels.AsEnumerable());
    }

    // Adds a new hotel to the repository
    public async Task AddAsync(Hotel hotel)
    {
      // If hotel is null, simply return without adding
      if (hotel == null)
      {
        // Optionally log or handle this scenario as needed
        return;
      }

      // Add hotel to in-memory list
      _hotels.Add(hotel);
      // Simulate asynchronous operation
      await Task.CompletedTask;
    }

    // Updates an existing hotel in the repository
    public async Task UpdateAsync(Hotel hotel)
    {
      // If hotel is null, simply return without updating
      if (hotel == null)
      {
        // Optionally log or handle this scenario as needed
        return;
      }

      // Find the index of the hotel to update
      var index = _hotels.FindIndex(h => h.Id == hotel.Id);
      if (index != -1)
      {
        // Replace existing hotel with updated one
        _hotels[index] = hotel;
      }
      // Simulate asynchronous operation
      await Task.CompletedTask;
    }

    // Deletes a hotel by its ID
    public async Task DeleteAsync(Guid id)
    {
      // Find the hotel to delete
      var hotel = _hotels.FirstOrDefault(h => h.Id == id);
      if (hotel != null)
      {
        // Remove hotel from in-memory list
        _hotels.Remove(hotel);
      }
      // Simulate asynchronous operation
      await Task.CompletedTask;
    }
  }
}
