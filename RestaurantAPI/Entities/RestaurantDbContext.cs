using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities;

public class RestaurantDbContext : DbContext
{
    // Declare properties that represents tables in Database.
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Dish> Dishes { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    
    //private const string ConnectionString = "Server=MAREKLAPTOP\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;";
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
        
        // Seed entities with data (populate with data).
        // This woul require also adding migration fie that will populate tables with proper data.
        // modelBuilder.Entity<Restaurant>().HasData(
        //     new Restaurant 
        //     { 
        //         Id = 1, 
        //         Name = "<NAME>", 
        //         Description = "Pizza", 
        //         Category = "Italian", 
        //         AddressId = 1, 
        //         Email = "<EMAIL>", 
        //         PhoneNumber = "123456789", 
        //         HasDelivery = true }
        // );
    }
}