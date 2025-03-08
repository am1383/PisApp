using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Interfaces
{
    public interface ICompatibleRepository
    {
        public Task<List<Product>> CompatibleWithCPU(int cpuId, int maxMemoryLimit, decimal baseFrequency, decimal BoostFrequncy, int supportedWattage);
        public Task<List<Product>> CompatibleWithMotherboard(int motherboardId, decimal memorySpeedRange, int supportedWattage);
        public Task<List<Product>> CompatibleWithRAM(int ramId, decimal frequency, decimal capacity, int supportedWattage);
        public Task<List<Product>> GetCompatiblePartsAsync(IEnumerable<int> selectedPartIds);
        public Task<List<Product>> CompatbileWithCooler(int coolerId, int supportedWattage);
        public Task<List<Product>> CompatibleWithSSD(int ssdId, int supportedWattage);
        public Task<List<Product>> CompatibleWithGPU(int gpuId, int supportedWattage);
        public Task<List<Product>>ComptaibleWithHDD(int supportedWattage);
    }
}