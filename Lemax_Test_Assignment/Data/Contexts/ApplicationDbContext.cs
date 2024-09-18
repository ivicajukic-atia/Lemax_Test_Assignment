using Lemax_Test_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Lemax_Test_Assignment.Data.Contexts
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

    }
  }
}
