using FluentValidation;
using PisApp.API.DTOs.LoginDto;

namespace MRH.Backend.Customers.Services.DTOs.Validators
{
    public class AddScoreValidation : AbstractValidator<LoginDto>
    {
        public AddScoreValidation()
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