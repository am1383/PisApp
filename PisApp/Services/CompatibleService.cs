using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Services
{
    public class CompatibleService(IUnitOfWork unitOfWork) : ICompatibleService
    {
        public async Task<IEnumerable<ProductDetailsDto>> GetCompaitblesHandler(List<CompatibleDetailsDto> dto, string? type)
        {
            var productIds = dto.Select(c => c.products_id).ToList();

            var compatibleParts          = await GetCompatibleParts(productIds);
            var compatibleAttributeParts = await GetCompatibleAttributeParts(dto);
            
            var result = compatibleParts.Concat(compatibleAttributeParts)
                                        .DistinctBy(p => p.product_id);

            var filteredParts = result.Where(p => string.IsNullOrEmpty(type) || p.category == type);

            return filteredParts.Select(p => new ProductDetailsDto
            {
                product_id = p.product_id,
                model      = p.model,
                brand      = p.brand,
                category   = p.category,
            });
        }

        private async Task<IEnumerable<ProductDetailsDto>> GetCompatibleParts(IEnumerable<int> selectedPartIds)
        {
            var selectedParts   = selectedPartIds.ToArray();

            var compatibleParts = await unitOfWork.Compatibles.GetCompatiblePartsAsync(selectedParts);

            return compatibleParts.Select(p => new ProductDetailsDto
            {
                product_id = p.product_id,
                model      = p.model,
                brand      = p.brand,
                category   = p.category,
            });
        }

        private async Task<IEnumerable<ProductDetailsDto>> GetCompatibleAttributeParts(List<CompatibleDetailsDto> dto)
        {
            var compatibleProducts = new List<Product>();

            var productData = dto.Select(c => new { c.products_id, c.category }).ToList();

            foreach (var productId in productData)
            {
                List<Product> products = productId.category switch
                {
                    "CPU"         => await CompatibleWithCPUHandler(productId.products_id),
                    "Motherboard" => await CompatibleWithMotherbordHandler(productId.products_id),
                    "RAM Stick"   => await CompatibleWithRAMHandler(productId.products_id),
                    "Cooler"      => await CompatibleWithCoolerHandler(productId.products_id),
                    "SSD"         => await CompatibleWithSSDHandler(productId.products_id),
                    "GPU"         => await CompatibleWithGPUHandler(productId.products_id),
                    "HDD"         => await CompatibleWithHHDHandler(productId.products_id),
                    _             => new List<Product>()
                };

                compatibleProducts.AddRange(products);
            }

            return compatibleProducts.Select(p => new ProductDetailsDto
            {
                product_id = p.product_id,
                model      = p.model,
                brand      = p.brand,
                category   = p.category,
            });
        }

        private async Task<List<Product>> CompatibleWithCPUHandler(int productId)
        {
            var cpu = await unitOfWork.Products.GetCpuDetails(productId);

            return await unitOfWork.Compatibles.CompatibleWithCPU(productId, cpu.max_memory_limit, cpu.base_frequency, cpu.boost_frequency, cpu.supported_wattage);
        }

        private async Task<List<Product>> CompatibleWithMotherbordHandler(int productId)
        {
            var motherboard = await unitOfWork.Products.GetMotherboardDetails(productId);

            return await unitOfWork.Compatibles.CompatibleWithMotherboard(productId, motherboard.memory_speed_range, motherboard.wattage);
        }

        private async Task<List<Product>> CompatibleWithRAMHandler(int productId)
        {
            var ram = await unitOfWork.Products.GetRAMDetails(productId);

            return await unitOfWork.Compatibles.CompatibleWithRAM(productId, ram.frequency, ram.capacity, ram.wattage);
        }

        private async Task<List<Product>> CompatibleWithCoolerHandler(int productId)
        {
            var cooler = await unitOfWork.Products.GetCoolerDetails(productId);

            return await unitOfWork.Compatibles.CompatbileWithCooler(productId, cooler.wattage);
        }

        private async Task<List<Product>> CompatibleWithSSDHandler(int productId)
        {
            var ssd = await unitOfWork.Products.GetSsdDetails(productId);

            return await unitOfWork.Compatibles.CompatibleWithSSD(productId, ssd.wattage);
        }

        private async Task<List<Product>> CompatibleWithGPUHandler(int productId)
        {
            var gpu = await unitOfWork.Products.GetGpuDetails(productId);

            return await unitOfWork.Compatibles.CompatibleWithGPU(productId, gpu.wattage);
        }

        private async Task<List<Product>> CompatibleWithHHDHandler(int productId)
        {
            var hdd = await unitOfWork.Products.GetHddDetails(productId);

            return await unitOfWork.Compatibles.ComptaibleWithHDD(hdd.wattage);
        }
    }
}