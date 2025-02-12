using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.DTOs;
using PisApp.API.Entities;
using PisApp.API.Interface;

namespace PisApp.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PisAppDbContext _context;

        public AddressRepository(PisAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddressDetailDto>> GetAllAddressesById(int userId)
        {
            var query = "SELECT province, remain_address FROM address_of_client WHERE client_id = {0}";

            return await _context.Set<Address>()
                .FromSqlRaw(query, userId)
                .Select(a => new AddressDetailDto
                {
                    province = a.province,      
                    remain_address = a.remain_address 
                })
                .ToListAsync();
        }
    }
}