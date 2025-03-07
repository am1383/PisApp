using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
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

        public async Task<List<CartItemProduct>> GetCartItemProducts(int lockedNumber)
        {
            var query = @"SELECT p.brand, p.model, p.category, a.cart_price AS price, a.quantity
                          FROM added_to a
                          JOIN product p ON a.product_id = p.id
                          WHERE a.locked_number          = @p0";

            return await unitOfWork.Context.Set<CartItemProduct>()
                                           .FromSqlRaw(query, lockedNumber)
                                           .ToListAsync();
        }
    }   
}