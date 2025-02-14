using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Dtos;
using PisApp.API.Products.Entities;

namespace PisApp.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TDto>> GetAllProducts<TEntity, TDto>(
            Func<IProductRepository, Task<IEnumerable<TEntity>>> getMethod,
            Func<TEntity, TDto> mapFunction)
        {
            var entities = await getMethod(_unitOfWork.Products);
            return entities.Select(mapFunction);
        }

        public async Task<IEnumerable<MotherboardDetailsDto>> GetAllMotherboard()
        {
            return await GetAllProducts(
                repo => repo.GetAllMotherboardAsync(),
                m => new MotherboardDetailsDto
                {
                    wattage = m.wattage,
                    chipset_name = m.chipset_name,
                    num_memory_slots = m.num_memory_slots,
                    memory_speed_range = m.memory_speed_range,
                    depth = m.depth,
                    height = m.height,
                    width = m.width
                });
        }

        public async Task<IEnumerable<CpuDetailsDto>> GetAllCpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllCpuAsync(),
                c => new CpuDetailsDto
                {
                    max_memory_limit = c.max_memory_limit,
                    wattage = c.wattage,
                    generation = c.generation,
                    microarchitecture = c.microarchitecture,
                    num_cores = c.num_cores,
                    num_threads = c.num_threads,
                    base_frequency = c.base_frequency,
                    boost_frequency = c.boost_frequency
                });
        }

        public async Task<IEnumerable<RamDetailDto>> GetAllRam()
        {
            return await GetAllProducts(
                repo => repo.GetAllRamAsync(),
                r => new RamDetailDto
                {
                    wattage = r.wattage,
                    generation = r.generation,
                    capacity = r.capacity,
                    frequency = r.frequency,
                    depth = r.depth,
                    height = r.height,
                    width = r.width
                });
        }

        public async Task<IEnumerable<GpuDetailsDto>> GetAllGpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllGpuAsync(),
                g => new GpuDetailsDto
                {
                    ram_size = g.ram_size,
                    wattage = g.wattage,
                    num_fans = g.num_fans,
                    clock_speed = g.clock_speed,
                    depth = g.depth,
                    height = g.height,
                    width = g.wattage
                });
        }

        public async Task<IEnumerable<SsdDetailDto>> GetAllSsd()
        {
            return await GetAllProducts(
                repo => repo.GetAllSsdAsync(),
                s => new SsdDetailDto
                {
                    wattage = s.wattage,
                    capacity = s.capacity,
                });
        }

        public async Task<IEnumerable<PowerSupplyDetailsDto>> GetAllPowerSupply()
        {
            return await GetAllProducts(
                repo => repo.GetAllPowerSupplyAsync(),
                p => new PowerSupplyDetailsDto
                {
                    supported_wattage = p.supported_wattage,
                    depth = p.depth,
                    height = p.height,
                    width = p.width,
                });
        }

        public async Task<IEnumerable<CoolerDetailsDto>> GetAllCooler()
        {
            return await GetAllProducts(
                repo => repo.GetAllCoolerAsync(),
                co => new CoolerDetailsDto
                {
                    cooling_method = co.cooling_method,
                    fan_size = co.fan_size,
                    max_rotational_speed = co.max_rotational_speed,
                    wattage = co.wattage,
                    depth = co.depth,
                    height = co.height,
                    width = co.width,   
                }
            );
        }
    }
}
