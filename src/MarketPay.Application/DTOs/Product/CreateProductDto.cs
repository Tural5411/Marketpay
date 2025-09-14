using System.ComponentModel.DataAnnotations;

namespace MarketPay.Application.DTOs.Product;

public class CreateProductDto
{
    [Required(ErrorMessage = "Market ID zorunludur")]
    public Guid MarketId { get; set; }

    [Required(ErrorMessage = "Ürün barkodu zorunludur")]
    [StringLength(200, ErrorMessage = "Ürün barkodu en fazla 200 karakter olabilir")]
    public string ProductBarcode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ürün adı zorunludur")]
    [StringLength(200, ErrorMessage = "Ürün adı en fazla 200 karakter olabilir")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fiyat zorunludur")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
    public decimal ProductPrice { get; set; }

    [Required(ErrorMessage = "Ürün birimi zorunludur")]
    [StringLength(50, ErrorMessage = "Ürün birimi en fazla 50 karakter olabilir")]
    public string ProductUnit { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

