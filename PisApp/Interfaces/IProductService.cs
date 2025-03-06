using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<TDto>> GetAllProducts<TEntity, TDto>(
            Func<IProductRepository, Task<IEnumerable<TEntity>>> getMethod,
            Func<TEntity, TDto> mapFunction);
        public Task<IEnumerable<CommonProductsDto>> GetAllMotherboard();
        public Task<IEnumerable<CommonProductsDto>> GetAllPowerSupply();
        public Task<IEnumerable<CommonProductsDto>> GetAllCooler();
        public Task<IEnumerable<CommonProductsDto>> GetAllCpu();
        public Task<IEnumerable<CommonProductsDto>> GetAllGpu();
        public Task<IEnumerable<CommonProductsDto>> GetAllRam();
        public Task<IEnumerable<CommonProductsDto>> GetAllSsd();
    }
}