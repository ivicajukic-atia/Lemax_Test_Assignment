using Lemax_Test_Assignment.Models;

namespace Lemax_Test_Assignment.Data.Interfaces
{
  public interface IHotelRepository
  {
    Task<Hotel> GetByIdAsync(Guid id);
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid id);
  }
}
