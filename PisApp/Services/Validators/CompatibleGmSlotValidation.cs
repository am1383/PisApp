using FluentValidation;
using PisApp.API.Compatibles.Dtos;

namespace PisApp.API.Services.Dtos.Validators
{
    public class CompatibleGmSlotValidation : AbstractValidator<CompatibleGmSlotRequestDto>
    {
        public CompatibleGmSlotValidation()
        {
            RuleFor(c => c.gpu_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
            
            RuleFor(c => c.motherboard_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
        }
    }
}