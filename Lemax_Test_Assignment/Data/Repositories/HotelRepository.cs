using Lemax_Test_Assignment.Data.Contexts;
using Lemax_Test_Assignment.Data.Interfaces;
using Lemax_Test_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemax_Test_Assignment.Data.Repositories
{
  public class HotelRepository : IHotelRepository
  {
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Hotel> GetByIdAsync(Guid id)
    {
      // Retrieve a hotel by its unique identifier from the database
      return await _context.Hotels.FindAsync(id);
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
      // Retrieve all hotels from the database
      return await _context.Hotels.ToListAsync();
    }

    public async Task AddAsync(Hotel hotel)
    {
      if (hotel == null)
      {
        throw new ArgumentNullException(nameof(hotel));
      }

      // Add a new hotel to the database
      _context.Hotels.Add(hotel);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hotel hotel)
    {
      if (hotel == null)
      {
        throw new ArgumentNullException(nameof(hotel));
      }

      // Update an existing hotel in the database
      _context.Hotels.Update(hotel);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
      // Find the hotel in the database and remove it
      var hotel = await _context.Hotels.FindAsync(id);
      if (hotel != null)
      {
        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
      }
    }
  }
}
