using PisApp.API.Dtos;
using PisApp.API.Products.Entities;

namespace PisApp.API.Interfaces
{
    public interface ICompatibleService
    {
        public Task<IEnumerable<ProductDetailsDto>> GetCompaitblesHandler(List<CompatibleDetailsDto> dto, string? type);
    }
}