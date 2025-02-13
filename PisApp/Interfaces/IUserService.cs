using PisApp.API.DTOs;
using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IUserService
    {
        public Task<int> FindUserIdByPhoneNumber(string phoneNumber);
        public Task<DiscountSummaryDto> UserDiscountsSummary(IEnumerable<PrivateDiscountDetailsDto> privateCodes, GiftDiscountDetailDto giftCodes);
        public Task<UserDetail> GetUserDetailsById(int userId);
        public Task<IEnumerable<CartDetailsDto>> UserCartsStatus(int userId);
        public Task<int> CountUserRefferer(string referCode);
        public Task<IEnumerable<PrivateDiscountDetailsDto>> UserPrivateCodeWithLimiteTime(int userId);
        public  Task<ShoppingCartsDetailsDto> UserRecentPurchases(int userId);
        public Task<VIPUserDetailDto> GetRemainingTimeForVIP(int userId);
        public Task<GiftDiscountDetailDto> UserGiftedCodeCount(int userId);
        public Task<UserDetailDto> Details(UserDetail user, VIPUserDetailDto isUserVIP, int countUserReffer);
        public  Task<UserProfitDto> VIPUserProfit(int userId);
        public Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId);
    }
}