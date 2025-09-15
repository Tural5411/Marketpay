using FluentValidation;
using MarketPay.Application.DTOs.Cart;

namespace MarketPay.Application.Validators;

public class CreateCartDtoValidator : AbstractValidator<CreateCartDto>
{
    public CreateCartDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Kullanıcı ID zorunludur");

        RuleFor(x => x.MarketId)
            .NotEmpty().WithMessage("Market ID zorunludur");
    }
}
