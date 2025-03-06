using PisApp.API.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<CommonProduct>> GetAllMotherboardAsync();
        public Task<IEnumerable<CommonProduct>> GetAllPowerSupplyAsync();
        public Task<List<CartItemProduct>> GetCartItemProducts(int lockedNumber);
        public Task<IEnumerable<CommonProduct>> GetAllCoolerAsync();
        public Task<IEnumerable<CommonProduct>> GetAllCpuAsync();
        public Task<IEnumerable<CommonProduct>> GetAllGpuAsync();
        public Task<IEnumerable<CommonProduct>> GetAllRamAsync();
        public Task<IEnumerable<CommonProduct>> GetAllSsdAsync();
    }
}