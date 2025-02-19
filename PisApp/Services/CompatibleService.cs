using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;

namespace PisApp.API.Services
{
    public class CompatibleService(IUnitOfWork unitOfWork) : ICompatibleService
    {
        public async Task<IEnumerable<ProductDetailsDto>> GetCompatibleParts(List<int> selectedPartIds, string type)
        {
            var selectedPartsTable = string.Join(",", selectedPartIds);

            var compatibleParts = await unitOfWork.Compatibles.GetCompatiblePartsAsync(selectedPartsTable, type);

            return compatibleParts.Select(p => new ProductDetailsDto
            {
                model         = p.model,
                brand         = p.brand,
                category      = p.category,
                current_price = p.current_price,
                stock_count   = p.stock_count
            });
        }
    }
}