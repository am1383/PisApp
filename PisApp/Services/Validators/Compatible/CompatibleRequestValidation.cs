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
                .WithMessage("{PropertyName} را وارد کنید.")
                .Must(HaveCorrectSize)
                .WithMessage("{PropertyName} باید حداقل یک مقدار داشته باشد.")
                .Must(BeUnique)
                .WithMessage("{PropertyName} نباید مقدار تکراری داشته باشد.");
        }

        private bool HaveCorrectSize(List<int> productsId)
        {
            var minProductsCount = 1;

            return productsId?.Count >= minProductsCount;
        }

        private bool BeUnique(List<int> productsId)
        {
            return productsId.Distinct().Count() == productsId.Count;
        }
    }
}
