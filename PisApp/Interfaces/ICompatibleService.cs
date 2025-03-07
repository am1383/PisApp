using PisApp.API.Products.Entities;

namespace PisApp.API.Interfaces
{
    public interface ICompatibleService
    {
        public Task<IEnumerable<ProductDetailsDto>> GetCompatibleParts(IEnumerable<int> selectedPartIds, string? type);
    }
}