using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities;

public class RestaurantDbContext : DbContext
{
    // Declare properties that represent tables in Database.
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Dish> Dishes { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    
    private const string _CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connect to MS SQL Database.
        optionsBuilder.UseSqlServer(_CONNECTION_STRING);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Restaurant entity.
        modelBuilder.Entity<Restaurant>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(25);
        // Configure the Dish entity.
        modelBuilder.Entity<Dish>()
            .Property(d => d.Name)
            .IsRequired();
        // Configure the Address entity.
        modelBuilder.Entity<Address>()
            .Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Address>()
            .Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(50);
    }
}