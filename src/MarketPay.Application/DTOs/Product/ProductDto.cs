namespace MarketPay.Application.DTOs.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid MarketId { get; set; }
    public string ProductBarcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string ProductUnit { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string MarketName { get; set; } = string.Empty;
}

