using PisApp.API.DTOs;

namespace PisApp.API.Interface
{
    public interface IUserRepository
    {
        public Task<bool> GetUserByPhoneNumberAsync(string phoneNumber);

        public Task<UserDetailDto> GetUserDetailAsync(int userId);

        public Task<DateTime> VIPChecker(int userId);

        public Task<int> GetUserId(string phoneNumber);
    }
}