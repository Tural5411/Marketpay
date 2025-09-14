using FluentValidation;
using MarketPay.Application.DTOs.Product;

namespace MarketPay.Application.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.ProductBarcode)
            .NotEmpty().WithMessage("Ürün barkodu zorunludur")
            .MaximumLength(200).WithMessage("Ürün barkodu en fazla 200 karakter olabilir")
            .MinimumLength(3).WithMessage("Ürün barkodu en az 3 karakter olmalıdır");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Ürün adı zorunludur")
            .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir")
            .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır");

        RuleFor(x => x.ProductPrice)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(999999.99m).WithMessage("Fiyat çok yüksek");

        RuleFor(x => x.ProductUnit)
            .NotEmpty().WithMessage("Ürün birimi zorunludur")
            .MaximumLength(50).WithMessage("Ürün birimi en fazla 50 karakter olabilir");
    }
}

