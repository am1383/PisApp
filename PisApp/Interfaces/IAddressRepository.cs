using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IAddressRepository
    {
        public Task<List<Address>> GetUserAddressesAsync(int userId);
    }
}