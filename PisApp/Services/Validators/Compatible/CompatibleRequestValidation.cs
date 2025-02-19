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
                .Must(HaveCorrectSize)
                .WithMessage("{PropertyName} باید حداقل دو مقدار داشته باشد.")
                .NotEmpty()
                .WithMessage("{PropertyName} را وارد کنید.");
        }

        private bool HaveCorrectSize(List<int> productsId)
        {
            var minSize = 2;

            return productsId != null && productsId.Count >= minSize;
        }
    }
}