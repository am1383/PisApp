using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Repositories
{
    public class ProductRepository(IUnitOfWork unitOfWork) : IProductRepository
    {
        public async Task<IEnumerable<CommonProduct>> GetAllMotherboardAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM motherboard 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllCpuAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM cpu 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllRamAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM ram_stick 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllGpuAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM gpu 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllSsdAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM ssd 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllCoolerAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM cooler 
                          JOIN product ON product_id = id";

            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query)
                                           .ToListAsync();
        }

        public async Task<IEnumerable<CommonProduct>> GetAllPowerSupplyAsync()
        {
            var query = @"SELECT product_id, brand, model 
                          FROM power_supply 
                          JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<CommonProduct>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<List<CartItemProduct>> GetCartItemProducts(int lockedNumber, int userId)
        {
            var query = @"SELECT 
                        p.brand, p.model, p.category, a.cart_price AS price, a.quantity
                        FROM added_to a
                        JOIN product p ON a.product_id = p.id
                        WHERE a.client_id              = @p0
                        AND a.locked_number            = @p1";

            return await unitOfWork.Context.Set<CartItemProduct>()
                                           .FromSqlRaw(query, userId, lockedNumber)
                                           .ToListAsync();
        }

        public async Task<Cpu> GetCpuDetails(int productId)
        {
            var query = @"SELECT max_memory_limit, wattage,
                          base_frequency, boost_frequency 
                          FROM cpu 
                          WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Cpu>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<Gpu> GetGpuDetails(int productId)
        {
            var query = @"SELECT wattage FROM gpu WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Gpu>()
                                            .FromSqlRaw(query, productId)
                                            .FirstOrDefaultAsync();
        }

        public async Task<Ssd> GetSsdDetails(int productId)
        {
            var query = @"SELECT wattage FROM ssd WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Ssd>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<Ram> GetRAMDetails(int productId)
        {
            var query = @"SELECT wattage, capacity, frequency
                          FROM ram 
                          WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Ram>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<Motherboard> GetMotherboardDetails(int productId)
        {
            var query = @"SELECT wattage, memory_speed_range
                          FROM motherboard 
                          WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Motherboard>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<Cooler> GetCoolerDetails(int productId)
        {
            var query = @"SELECT wattage FROM cooler WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Cooler>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<Hdd> GetHddDetails(int productId)
        {
            var query = @"SELECT wattage FROM hdd WHERE product_id = @p0";

            return await unitOfWork.Context.Set<Hdd>()
                                           .FromSqlRaw(query, productId)
                                           .FirstOrDefaultAsync();
        }
    }   
}