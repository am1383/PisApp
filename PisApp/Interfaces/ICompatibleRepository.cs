using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Interfaces
{
    public interface ICompatibleRepository
    {
        public Task<List<Product>> GetCompatiblePartsAsync(int[] selectedPartIds, string? partType);
    }
}