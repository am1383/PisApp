using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> GetUserByPhoneNumberAsync(string phoneNumber);
        public Task<UserDetail> GetUserDetailAsync(int userId);
        public Task<DateTime> VIPChecker(int userId);
        public Task<int> GetUserId(string phoneNumber);
    }
}