using Microsoft.EntityFrameworkCore;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities;

namespace PisApp.API.Repositories
{
    public class ProductRepository(IUnitOfWork unitOfWork) : IProductRepository
    {
        public async Task<IEnumerable<Motherboard>> GetAllMotherboardAsync()
        {
            var query = "SELECT * FROM motherboard";
 
            return await unitOfWork.Context.Set<Motherboard>()
                                            .FromSqlRaw(query) 
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Cpu>> GetAllCpuAsync()
        {
            var query = "SELECT * FROM cpu";
 
            return await unitOfWork.Context.Set<Cpu>()
                                            .FromSqlRaw(query) 
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Ram>> GetAllRamAsync()
        {
            var query = "SELECT * FROM ram_stick";
 
            return await unitOfWork.Context.Set<Ram>()
                                            .FromSqlRaw(query) 
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Gpu>> GetAllGpuAsync()
        {
            var query = "SELECT * FROM gpu";
 
            return await unitOfWork.Context.Set<Gpu>()
                                            .FromSqlRaw(query) 
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Ssd>> GetAllSsdAsync()
        {
            var query = "SELECT * FROM ssd";
 
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
            var query = "SELECT * FROM power_supply";
 
            return await unitOfWork.Context.Set<PowerSupply>()
                                            .FromSqlRaw(query) 
                                            .ToListAsync();
        }
    }   
}