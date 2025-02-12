using PisApp.API.DTOs;

namespace PisApp.API.Interface
{
    public interface IUserService
    {
        public Task<int> FindUserIdByPhoneNumber(string phoneNumber);

        public Task<UserDetailDto> GetUserDetailsById(int userId);

        public Task<VIPUserDetailDto> GetRemainingTimeForVIP(int userId);

        public Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId);
    }
}