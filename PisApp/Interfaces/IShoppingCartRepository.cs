using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task<List<ShoppingCart>> UserRecentPurchasesAsync(int userId);
        public Task<List<Cart>> UserCartsStatus(int userId);
        public Task<int> AvailabeUserCarts(int userId);
    }
}