using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> GetUserByPhoneNumberAsync(string phoneNumber);
        public Task<UserDetail> GetUserDetailAsync(int userId);
        public Task<string> GetUserRefferCode(int userId);
        public Task<int> GetUserIdByPhoneNumberAsync(string phoneNumber);
        public Task<DateTime> VIPChecker(int userId);
        public Task<bool> isUserVIP(int userId);
    }
}