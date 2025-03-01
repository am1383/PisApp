using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Dtos;
using PisApp.API.Products.Entities;

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

        public async Task<IEnumerable<MotherboardDetailsDto>> GetAllMotherboard()
        {
            return await GetAllProducts(
                repo => repo.GetAllMotherboardAsync(),
                m => new MotherboardDetailsDto
                {
                    product_id   = m.product_id,
                    wattage      = m.wattage,
                    chipset_name = m.chipset_name,
                    num_memory_slots   = m.num_memory_slots,
                    memory_speed_range = m.memory_speed_range,
                    depth    = m.depth,
                    height   = m.height,
                    width    = m.width,
                    model    = m.model,
                    brand    = m.brand,
                    category = m.category,
                    current_price = m.current_price,
                    stock_count   = m.stock_count
                });
        }

        public async Task<IEnumerable<CpuDetailsDto>> GetAllCpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllCpuAsync(),
                c => new CpuDetailsDto
                {
                    product_id        = c.product_id,
                    max_memory_limit  = c.max_memory_limit,
                    wattage           = c.wattage,
                    generation        = c.generation,
                    microarchitecture = c.microarchitecture,
                    num_cores         = c.num_cores,
                    num_threads       = c.num_threads,
                    base_frequency    = c.base_frequency,
                    boost_frequency   = c.boost_frequency,
                    model             = c.model,
                    brand             = c.brand,
                    category          = c.category,
                    current_price     = c.current_price,
                    stock_count       = c.stock_count
                });
        }

        public async Task<IEnumerable<RamDetailDto>> GetAllRam()
        {
            return await GetAllProducts(
                repo => repo.GetAllRamAsync(),
                r => new RamDetailDto
                {
                    product_id = r.product_id,
                    wattage    = r.wattage,
                    generation = r.generation,
                    capacity   = r.capacity,
                    frequency  = r.frequency,
                    depth      = r.depth,
                    height     = r.height,
                    width      = r.width,
                    model      = r.model,
                    brand      = r.brand,
                    category   = r.category,
                    current_price = r.current_price,
                    stock_count   = r.stock_count
                });
        }

        public async Task<IEnumerable<GpuDetailsDto>> GetAllGpu()
        {
            return await GetAllProducts(
                repo => repo.GetAllGpuAsync(),
                g => new GpuDetailsDto
                {
                    product_id  = g.product_id,
                    ram_size    = g.ram_size,
                    wattage     = g.wattage,
                    num_fans    = g.num_fans,
                    clock_speed = g.clock_speed,
                    depth    = g.depth,
                    height   = g.height,
                    width    = g.wattage,
                    model    = g.model,
                    brand    = g.brand,
                    category = g.category,
                    current_price = g.current_price,
                    stock_count   = g.stock_count
                });
        }

        public async Task<IEnumerable<SsdDetailDto>> GetAllSsd()
        {
            return await GetAllProducts(
                repo => repo.GetAllSsdAsync(),
                s => new SsdDetailDto
                {
                    product_id = s.product_id,
                    wattage    = s.wattage,
                    capacity   = s.capacity,
                    model      = s.model,
                    brand      = s.brand,
                    category   = s.category,
                    current_price = s.current_price,
                    stock_count   = s.stock_count
                });
        }

        public async Task<IEnumerable<PowerSupplyDetailsDto>> GetAllPowerSupply()
        {
            return await GetAllProducts(
                repo => repo.GetAllPowerSupplyAsync(),
                p => new PowerSupplyDetailsDto
                {
                    product_id        = p.product_id,
                    supported_wattage = p.supported_wattage,
                    depth  = p.depth,
                    height = p.height,
                    width  = p.width,
                    model  = p.model,
                    brand  = p.brand,
                    category      = p.category,
                    current_price = p.current_price,
                    stock_count   = p.stock_count
                });
        }

        public async Task<IEnumerable<CoolerDetailsDto>> GetAllCooler()
        {
            return await GetAllProducts(
                repo => repo.GetAllCoolerAsync(),
                co => new CoolerDetailsDto
                {
                    product_id     = co.product_id,
                    cooling_method = co.cooling_method,
                    fan_size       = co.fan_size,
                    max_rotational_speed = co.max_rotational_speed,
                    wattage = co.wattage,
                    depth   = co.depth,
                    height  = co.height,
                    width   = co.width, 
                    model   = co.model,
                    brand   = co.brand,
                    category      = co.category,
                    current_price = co.current_price,
                    stock_count   = co.stock_count  
                }
            );
        }
    }
}