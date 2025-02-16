using FluentValidation;
using PisApp.API.Dtos.LoginDto;

namespace PisApp.API.Services.Dtos.Validators
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(p => p.phone_number)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.")
                .Matches("^(\\+98|0)?9(1[0-9]|3[0-9]|0[0-9]|99[0-9])\\d{7}$")
                .WithMessage("فرمت شماره موبایل درست نمیباشد.");
        }
    }
}