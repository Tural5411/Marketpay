using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class SupportChat : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Sender { get; set; } = string.Empty; // "user" or "support"

    // Navigation properties
    public User User { get; set; } = null!;
}
