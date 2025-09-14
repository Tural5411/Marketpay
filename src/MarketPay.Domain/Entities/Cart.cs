using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class Cart : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid MarketId { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "active"; // active, completed

    // Navigation properties
    public User User { get; set; } = null!;
    public Market Market { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public Payment? Payment { get; set; }
}
