using PisApp.API.Dtos;
using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IUserService
    {
        public Task<int> FindUserIdByPhoneNumber(string phoneNumber);
        public DiscountSummaryDto UserDiscountsSummary(IEnumerable<PrivateDiscountDetailsDto> privateCodes, GiftDiscountDetailDto giftCodes);
        public Task<UserDetail> GetUserDetailsById(int userId);
        public Task<CartResponseDto> UserCartsStatus(int userId);
        public Task<int> CountUserRefferer(string referCode);
        public Task<IEnumerable<PrivateDiscountDetailsDto>> UserPrivateCodeWithLimiteTime(int userId);
        public Task<IEnumerable<ShoppingCartsDetailsDto>> UserRecentPurchases(int userId);
        public Task<VIPUserDetailDto> GetRemainingTimeForVIP(int userId);
        public Task<GiftDiscountDetailDto> UserGiftedCodeCount(int userId);
        public UserDetailDto Details(UserDetail user, VIPUserDetailDto isUserVIP, int countUserReffer);
        public Task<UserProfitDto> VIPUserProfit(int userId);
        public Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId);
        public Task<bool> isUserVIPChecker(int userId);
    }
}