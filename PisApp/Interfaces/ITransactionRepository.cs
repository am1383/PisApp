using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<UserProfit> GetUserProfitForVIPClients(int userId);
    }
}