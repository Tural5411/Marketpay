using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<SupportChat> SupportChats { get; set; } = new List<SupportChat>();
}
