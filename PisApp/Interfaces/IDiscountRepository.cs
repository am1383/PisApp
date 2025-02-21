using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IDiscountRepository
    {
        public Task<List<PrivateDiscount>> GetPrivateDiscountCodesWithLessThanOneWeekLeft(int userId);
        public Task<int> GetGiftedDiscountCodesCount(string userRefferCode);
    }
}