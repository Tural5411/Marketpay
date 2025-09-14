using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class Payment : BaseEntity
{
    [Required]
    public Guid CartId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    public DateTime PaidAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Cart Cart { get; set; } = null!;
    public User User { get; set; } = null!;
}
