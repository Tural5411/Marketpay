using FluentValidation;
using MarketPay.Application.DTOs.SupportChat;

namespace MarketPay.Application.Validators;

public class CreateSupportChatDtoValidator : AbstractValidator<CreateSupportChatDto>
{
    public CreateSupportChatDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Kullanıcı ID zorunludur");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj zorunludur")
            .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir")
            .MinimumLength(1).WithMessage("Mesaj en az 1 karakter olmalıdır");

        RuleFor(x => x.Sender)
            .NotEmpty().WithMessage("Gönderen bilgisi zorunludur")
            .Must(x => x == "user" || x == "support").WithMessage("Gönderen 'user' veya 'support' olmalıdır");
    }
}
