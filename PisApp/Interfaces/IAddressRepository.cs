using PisApp.API.DTOs;

namespace PisApp.API.Interfaces
{
    public interface IAddressRepository
    {
        public Task<IEnumerable<AddressDetailDto>> GetAllAddressesById(int userId);
    }
}