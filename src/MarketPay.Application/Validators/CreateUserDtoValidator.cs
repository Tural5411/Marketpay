using FluentValidation;
using MarketPay.Application.DTOs.User;

namespace MarketPay.Application.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad soyad zorunludur")
            .MaximumLength(200).WithMessage("Ad soyad en fazla 200 karakter olabilir")
            .MinimumLength(2).WithMessage("Ad soyad en az 2 karakter olmalıdır");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
            .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon numarası zorunludur")
            .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir")
            .MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır");
    }
}
