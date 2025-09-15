using FluentValidation;
using MarketPay.Application.DTOs.CartItem;

namespace MarketPay.Application.Validators;

public class CreateCartItemDtoValidator : AbstractValidator<CreateCartItemDto>
{
    public CreateCartItemDtoValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty().WithMessage("Sepet ID zorunludur");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Ürün ID zorunludur");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(1000).WithMessage("Miktar en fazla 1000 olabilir");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(999999.99m).WithMessage("Fiyat çok yüksek");
    }
}
