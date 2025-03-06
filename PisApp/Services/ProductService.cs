using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<IEnumerable<TDto>> GetAllProducts<TEntity, TDto>(
            Func<IProductRepository, Task<IEnumerable<TEntity>>> getMethod,
            Func<TEntity, TDto> mapFunction)
        {
            var entities = await getMethod(unitOfWork.Products);
            return entities.Select(mapFunction);
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllMotherboard()
        {
            return await GetAllProducts(
                repo => repo.GetAllMotherboardAsync(),
                m => new CommonProductsDto
                {
                    product_id = m.product_id,
                    model    = m.model,
                    brand    = m.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllCpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllCpuAsync(),
                c => new CommonProductsDto
                {
                    product_id = c.product_id,
                    model      = c.model,
                    brand      = c.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllRam()
        {
            return await GetAllProducts(
                repo => repo.GetAllRamAsync(),
                r => new CommonProductsDto
                {
                    product_id = r.product_id,
                    model      = r.model,
                    brand      = r.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllGpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllGpuAsync(),
                g => new CommonProductsDto
                {
                    product_id = g.product_id,
                    model      = g.model,
                    brand      = g.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllSsd()
        {
            return await GetAllProducts(
                repo => repo.GetAllSsdAsync(),
                s => new CommonProductsDto
                {
                    product_id = s.product_id,
                    model      = s.model,
                    brand      = s.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllPowerSupply()
        {
            return await GetAllProducts(
                repo => repo.GetAllPowerSupplyAsync(),
                p => new CommonProductsDto
                {
                    product_id = p.product_id,
                    model      = p.model,
                    brand      = p.brand,
                });
        }

        public async Task<IEnumerable<CommonProductsDto>> GetAllCooler()
        {
            return await GetAllProducts(
                repo => repo.GetAllCoolerAsync(),
                co => new CommonProductsDto
                {
                    product_id = co.product_id,
                    model      = co.model,
                    brand      = co.brand,
                }
            );
        }
    }
}