using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Entities;
using PisApp.API.Interfaces;

namespace PisApp.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PisAppDbContext _context;

        public AddressRepository(PisAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAllAddressesById(int userId)
        {
            var query = "SELECT province, remain_address FROM address_of_client WHERE client_id = @p0";

            return await _context.Set<Address>()
                                 .FromSqlRaw(query, userId)
                                 .ToListAsync();
        }
    }
}