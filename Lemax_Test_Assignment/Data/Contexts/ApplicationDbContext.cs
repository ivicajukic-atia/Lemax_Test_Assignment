using Lemax_Test_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Lemax_Test_Assignment.Data.Contexts
{
  /// <summary>
  /// Represents the application's database context, which manages the database connection and 
  /// the mapping between the database and application models.
  /// </summary>
  public class ApplicationDbContext : DbContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the DbContext, including connection string and other settings.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of hotels, representing the collection of <see cref="Hotel"/> entities in the database.
    /// </summary>
    public DbSet<Hotel> Hotels { get; set; }

    /// <summary>
    /// Configures the model properties and relationships using the Fluent API.
    /// This method is called by the runtime when the model for a derived context is being created.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure entity properties and relationships here.
      // For example:
      // modelBuilder.Entity<Hotel>()
      //     .HasKey(h => h.Id);
      //
      // modelBuilder.Entity<Hotel>()
      //     .Property(h => h.Name)
      //     .IsRequired()
      //     .HasMaxLength(100);
    }
  }
}
