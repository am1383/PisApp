using FluentValidation;
using PisApp.API.Compatibles.Dtos;

namespace PisApp.API.Services.Dtos.Validators
{
    public class CompatibleGmConnectValidation : AbstractValidator<CompatibleGpConnectRequestDto>
    {
        public CompatibleGmConnectValidation()
        {
            RuleFor(c => c.gpu_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
            
            RuleFor(c => c.power_supply_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
        }
    }
}