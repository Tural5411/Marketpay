namespace MarketPay.Application.DTOs.Payment;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PaidAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
