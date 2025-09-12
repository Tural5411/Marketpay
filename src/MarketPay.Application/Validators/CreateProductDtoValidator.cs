using FluentValidation;
using MarketPay.Application.DTOs.Product;

namespace MarketPay.Application.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı zorunludur")
            .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir")
            .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(999999.99m).WithMessage("Fiyat çok yüksek");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Category)
            .MaximumLength(50).WithMessage("Kategori en fazla 50 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.Category));
    }
}

