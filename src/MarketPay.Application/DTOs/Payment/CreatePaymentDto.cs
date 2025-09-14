namespace MarketPay.Application.DTOs.Payment;

public class CreatePaymentDto
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
}
