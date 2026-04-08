namespace RestaurantAPI.Entities;

public class Restaurant
{
    // EntityFramework will automatically create PRIMARY KEY for this property.
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    // EntityFramework will automatically create FOREIGN KEY to other table for this property.
    public int AddressId { get; set; }
    // Create reference to Address.
    // It is easier later on to use Restaurant object, because we will have direct access to Address object, instead of searching it via ID.
    // Virtual used for future (if we want to Lazy Load entity in EntityFramework).
    public virtual required Address Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual List<Dish>? Dishes { get; set; }
    public bool HasDelivery { get; set; }
}