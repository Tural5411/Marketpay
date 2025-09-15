using FluentValidation;
using MarketPay.Application.DTOs.Market;

namespace MarketPay.Application.Validators;

public class CreateMarketDtoValidator : AbstractValidator<CreateMarketDto>
{
    public CreateMarketDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Market adı zorunludur")
            .MaximumLength(200).WithMessage("Market adı en fazla 200 karakter olabilir")
            .MinimumLength(2).WithMessage("Market adı en az 2 karakter olmalıdır");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Market kodu zorunludur")
            .MaximumLength(50).WithMessage("Market kodu en fazla 50 karakter olabilir")
            .MinimumLength(2).WithMessage("Market kodu en az 2 karakter olmalıdır")
            .Matches("^[A-Z0-9_]+$").WithMessage("Market kodu sadece büyük harf, rakam ve alt çizgi içerebilir");
    }
}
