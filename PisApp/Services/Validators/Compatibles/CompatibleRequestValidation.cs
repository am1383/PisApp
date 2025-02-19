using FluentValidation;
using PisApp.API.Dtos;

namespace PisApp.API.Services.Dtos.Validators
{
    public class CompatibleProductsRequestValidation : AbstractValidator<CompatibleProductsRequestDto>
    {
        public CompatibleProductsRequestValidation()
        {
            RuleFor(c => c.products_id)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
        }
    }
}