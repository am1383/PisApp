using PisApp.API.Entities;
using PisApp.API.Products.Entities;

namespace PisApp.API.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Motherboard>> GetAllMotherboardAsync();
        public Task<IEnumerable<PowerSupply>> GetAllPowerSupplyAsync();
        public Task<List<CartItemProduct>> GetCartItemProducts(int lockedNumber);
        public Task<IEnumerable<Cooler>> GetAllCoolerAsync();
        public Task<IEnumerable<Cpu>> GetAllCpuAsync();
        public Task<IEnumerable<Gpu>> GetAllGpuAsync();
        public Task<IEnumerable<Ram>> GetAllRamAsync();
        public Task<IEnumerable<Ssd>> GetAllSsdAsync();
    }
}