using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities;

public class Address
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public required string City { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Street { get; set; }
    public string? ZipCode { get; set; }
    public virtual Restaurant? Restaurant { get; set; }
}