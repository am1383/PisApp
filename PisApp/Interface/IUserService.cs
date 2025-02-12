namespace PisApp.API.Interface
{
    public interface IUserService
    {
        public Task<int> FindUserIdByPhoneNumber(string phoneNumber);
    }
}