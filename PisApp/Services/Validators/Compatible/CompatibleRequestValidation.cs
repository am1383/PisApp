using FluentValidation;
using PisApp.API.Dtos;

namespace PisApp.API.Services.Dtos.Validators
{
    public class CompatibleProductsRequestValidation : AbstractValidator<CompatibleRequestDto>
    {
        public CompatibleProductsRequestValidation()
        {
            RuleFor(c => c.compatibles)
                .NotNull()
                .WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.")
                .Must(c => HaveCorrectSize(c))
                .WithMessage("{PropertyName} باید حداقل یک مقدار داشته باشد.")
                .Must(c => BeUnique(c))
                .WithMessage("{PropertyName} نباید مقدار تکراری داشته باشد.");
        }

        private bool HaveCorrectSize(List<CompatibleDetailsDto> compatibles)
        {
            var minProductsCount = 1;

            return compatibles.Count >= minProductsCount;
        }

        private bool BeUnique(List<CompatibleDetailsDto> compatibles)
        {
            var productsIds = compatibles.Select(c => c.products_id).ToList();

            return productsIds.Distinct().Count() == productsIds.Count;
        }
    }
}
