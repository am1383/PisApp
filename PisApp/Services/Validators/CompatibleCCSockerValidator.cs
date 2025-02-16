using FluentValidation;
using PisApp.API.Compatibles.Dtos;

namespace PisApp.API.Services.Dtos.Validators
{
    public class CompatibleCCSocketValidation : AbstractValidator<CompatibleCCSocketRequestDto>
    {
        public CompatibleCCSocketValidation()
        {
            RuleFor(c => c.cpu_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
            
            RuleFor(c => c.cooler_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
        }
    }
}