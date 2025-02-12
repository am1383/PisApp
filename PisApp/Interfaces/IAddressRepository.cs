using PisApp.API.DTOs;

namespace PisApp.API.Interface
{
    public interface IAddressRepository
    {
        public Task<IEnumerable<AddressDetailDto>> GetAllAddressesById(int userId);
    }
}