using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Address>> GetAllAddressesById(int userId)
        {
            var query = "SELECT province, remain_address FROM address_of_client WHERE client_id = @p0";

            return await _unitOfWork.Context.Set<Address>()
                                            .FromSqlRaw(query, userId)
                                            .ToListAsync();
        }
    }
}