using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;

namespace PisApp.API.Services
{
    public class CompatibleService(IUnitOfWork unitOfWork) : ICompatibleService
    {
        public async Task<IEnumerable<ProductDetailsDto>> GetCompatibleParts(IEnumerable<int> selectedPartIds, string? type)
        {
            var selectedParts   = selectedPartIds.ToArray();

            var compatibleParts = await unitOfWork.Compatibles.GetCompatiblePartsAsync(selectedParts);

            var filteredParts   = compatibleParts.Where(p => string.IsNullOrEmpty(type) || p.category == type);

            return filteredParts.Select(p => new ProductDetailsDto
            {
                product_id = p.product_id,
                model      = p.model,
                brand      = p.brand,
                category   = p.category,
            });
        }
    }
}