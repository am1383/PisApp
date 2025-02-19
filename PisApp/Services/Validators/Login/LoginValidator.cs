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
                .Matches(@"^(09\d{9}|01[0-9]{9})$")
                .WithMessage("فرمت شماره موبایل درست نمیباشد.");
        }
    }
}