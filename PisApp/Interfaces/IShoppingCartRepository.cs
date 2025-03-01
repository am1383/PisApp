using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task<List<ShoppingCart>> GetRecentPurchasesNumberAsync(int userId);
        public Task<decimal> GetCartItemTotalPrice(int lockedNumber);
        public Task<List<Cart>> UserCartsStatus(int userId);

        public Task<int> AvailabeUserCarts(int userId);
    }
}