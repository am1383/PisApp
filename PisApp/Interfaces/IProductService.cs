using PisApp.API.Dtos;
using PisApp.API.Products.Dtos;
using PisApp.API.Products.Entities;

namespace PisApp.API.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<TDto>> GetAllProducts<TEntity, TDto>(
            Func<IProductRepository, Task<IEnumerable<TEntity>>> getMethod,
            Func<TEntity, TDto> mapFunction);
        public Task<IEnumerable<MotherboardDetailsDto>> GetAllMotherboard();
        public Task<IEnumerable<CpuDetailsDto>> GetAllCpu();
        public Task<IEnumerable<RamDetailDto>> GetAllRam();
        public Task<IEnumerable<GpuDetailsDto>> GetAllGpu();
        public Task<IEnumerable<CoolerDetailsDto>> GetAllCooler();
        public Task<IEnumerable<SsdDetailDto>> GetAllSsd();
        public Task<IEnumerable<PowerSupplyDetailsDto>> GetAllPowerSupply();
    }
}