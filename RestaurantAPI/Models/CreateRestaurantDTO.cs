using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models;

public class CreateRestaurantDTO
{
    [Required]
    [MaxLength(25)]
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool HasDelivery { get; set; }
    [Required]
    [MaxLength(50)]
    public required string City { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Street { get; set; }
    public string? ZipCode { get; set; }
}