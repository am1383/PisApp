namespace PisApp.API.Interface
{
    public interface IUserRepository
    {
        Task<bool> GetUserByPhoneNumberAsync(string phoneNumber);

        Task<int> GetUserId(string phoneNumber);
    }
}