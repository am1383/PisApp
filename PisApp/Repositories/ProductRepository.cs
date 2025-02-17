using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Interfaces;
using PisApp.API.Products.Entities;

namespace PisApp.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PisAppDbContext _context;

        public ProductRepository(PisAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Motherboard>> GetAllMotherboardAsync()
        {
            var query = "SELECT * FROM motherboard";
 
            return await _context.Set<Motherboard>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Cpu>> GetAllCpuAsync()
        {
            var query = "SELECT * FROM cpu";
 
            return await _context.Set<Cpu>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Ram>> GetAllRamAsync()
        {
            var query = "SELECT * FROM ram_stick";
 
            return await _context.Set<Ram>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Gpu>> GetAllGpuAsync()
        {
            var query = "SELECT * FROM gpu";
 
            return await _context.Set<Gpu>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Ssd>> GetAllSsdAsync()
        {
            var query = "SELECT * FROM ssd";
 
            return await _context.Set<Ssd>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Cooler>> GetAllCoolerAsync()
        {
            var query = "SELECT * FROM cooler";

            return await _context.Set<Cooler>()
                                 .FromSqlRaw(query)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<PowerSupply>> GetAllPowerSupplyAsync()
        {
            var query = "SELECT * FROM power_supply";
 
            return await _context.Set<PowerSupply>()
                                 .FromSqlRaw(query) 
                                 .ToListAsync();
        }
    }   
}