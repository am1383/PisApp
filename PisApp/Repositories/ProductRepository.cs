using Microsoft.EntityFrameworkCore;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Repositories
{
    public class ProductRepository(IUnitOfWork unitOfWork) : IProductRepository
    {
        public async Task<IEnumerable<Motherboard>> GetAllMotherboardAsync()
        {
            var query = "SELECT * FROM motherboard JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<Motherboard>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Cpu>> GetAllCpuAsync()
        {
            var query = "SELECT * FROM cpu JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<Cpu>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Ram>> GetAllRamAsync()
        {
            var query = "SELECT * FROM ram_stick JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<Ram>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Gpu>> GetAllGpuAsync()
        {
            var query = "SELECT * FROM gpu JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<Gpu>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Ssd>> GetAllSsdAsync()
        {
            var query = "SELECT * FROM ssd JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<Ssd>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Cooler>> GetAllCoolerAsync()
        {
            var query = "SELECT * FROM cooler JOIN product ON product_id = id";

            return await unitOfWork.Context.Set<Cooler>()
                                           .FromSqlRaw(query)
                                           .ToListAsync();
        }

        public async Task<IEnumerable<PowerSupply>> GetAllPowerSupplyAsync()
        {
            var query = "SELECT * FROM power_supply JOIN product ON product_id = id";
 
            return await unitOfWork.Context.Set<PowerSupply>()
                                           .FromSqlRaw(query) 
                                           .ToListAsync();
        }

        public async Task<List<Product>> GetCartItemProducts(int lockedNumber)
        {
            var query = @"
                    SELECT p.brand, p.model, p.category, p.current_price, p.stock_count
                    FROM added_to a
                    JOIN product p ON a.product_id = p.id
                    WHERE a.locked_number = @p0";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, lockedNumber)
                                           .ToListAsync();
        }
    }   
}