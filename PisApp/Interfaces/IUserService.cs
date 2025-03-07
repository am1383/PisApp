using PisApp.API.Dtos;
using PisApp.API.Entities;

namespace PisApp.API.Interfaces
{
    public interface IUserService
    {
        public DiscountSummaryDto MapUserDiscountsSummary(IEnumerable<PrivateDiscountDetailDto> privateCodes, GiftedDiscountDetailDto giftCodes);
        public UserDetailDto MapUserDetails(UserDetail user, VIPUserDetailDto isUserVIP, int countUserReffer);
        public Task<int> FindUserIdByPhoneNumber(string phoneNumber);
        public Task<UserDetail> GetUserDetailsById(int userId);
        public Task<CartResponseDto> GetUserCarts(int userId);
        public Task<int> CountUserRefferer(string referCode);
        public Task<IEnumerable<PrivateDiscountDetailDto>> UserPrivateCodeWithLimiteTime(int userId);
        public Task<IEnumerable<ShoppingCartsDetailsDto>> UserRecentPurchases(int userId);
        public Task<VIPUserDetailDto> GetVIPUserDetails(int userId);
        public Task<GiftedDiscountDetailDto> UserGiftedCodeCount(string userRefferCode);
        public Task<string> GetUserRefferCode(int userId);
        public Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId);
        public Task<bool> IsUserVIPChecker(int userId);
    }
}