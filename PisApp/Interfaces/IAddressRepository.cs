using PisApp.API.Dtos;
using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IAddressRepository
    {
        public Task<List<Address>> GetAllAddressesById(int userId);
    }
}