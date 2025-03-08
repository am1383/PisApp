using PisApp.API.Entities;
using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<CartItemProduct>> GetCartItemProducts(int lockedNumber, int userId);
        public Task<IEnumerable<CommonProduct>> GetAllMotherboardAsync();
        public Task<IEnumerable<CommonProduct>> GetAllPowerSupplyAsync();
        public Task<IEnumerable<CommonProduct>> GetAllCoolerAsync();
        public Task<IEnumerable<CommonProduct>> GetAllCpuAsync();
        public Task<IEnumerable<CommonProduct>> GetAllGpuAsync();
        public Task<IEnumerable<CommonProduct>> GetAllRamAsync();
        public Task<IEnumerable<CommonProduct>> GetAllSsdAsync();
        public Task<Cpu> GetCpuDetails(int productId);
        public Task<Motherboard> GetMotherboardDetails(int productId);
        public Task<Gpu> GetGpuDetails(int productId);
        public Task<Cooler> GetCoolerDetails(int productId);
        public Task<Ssd> GetSsdDetails(int productId);
        public Task<Ram> GetRAMDetails(int productId);
        public Task<Hdd> GetHddDetails(int productId);
    }
}