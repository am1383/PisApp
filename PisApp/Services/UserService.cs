using PisApp.API.Exceptions;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Entities;

namespace PisApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDetailDto Details(UserDetail user, VIPUserDetailDto isUserVIP, int countUserReffer)
        {
            return new UserDetailDto {
                first_name      = user.first_name,
                last_name       = user.last_name, 
                wallet_balance  = user.wallet_balance,
                referral_code   = user.referral_code,
                countUserReffer = countUserReffer,
                time_stamp      = user.FormattedTimeStamp,
                vip_detail      = isUserVIP,
            };
        }
        
        public async Task<int> FindUserIdByPhoneNumber(string phoneNumber)
        {
            if (await _unitOfWork.Users.GetUserByPhoneNumberAsync(phoneNumber))
            {   
                return await _unitOfWork.Users.GetUserId(phoneNumber);
            } 
            
            throw new UserNotFoundExceptions();
        }

        public async Task<UserDetail> GetUserDetailsById(int userId)
        {
            return await _unitOfWork.Users.GetUserDetailAsync(userId);
        }

        public async Task<VIPUserDetailDto> GetRemainingTimeForVIP(int userId)
        {
            var vipExpiryDate = await _unitOfWork.Users.VIPChecker(userId);
            
            if (vipExpiryDate == DateTime.MinValue)
            {
                return new VIPUserDetailDto
                {
                    is_VIP         = false, 
                    remaining_time = 0, 
                };
            }

            var remainingTime = vipExpiryDate - DateTime.UtcNow;
            var remainingDays = remainingTime.TotalDays < 0 ? 0 : (int)remainingTime.TotalDays;

            return new VIPUserDetailDto
            {
                is_VIP         = true, 
                remaining_time = remainingDays, 
            };
        }

        public async Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId)
        {
            var addresses = await _unitOfWork.Addresses.GetAllAddressesById(userId);

            return addresses.Select(a => new AddressDetailDto
            {
                province = a.province,
                remain_address = a.remain_address,
            });
        }

        public async Task<int> CountUserRefferer(string referCode)
        {
            return await _unitOfWork.Refers.CountUserReferrerByCode(referCode);
        }

        public async Task<IEnumerable<PrivateDiscountDetailsDto>> UserPrivateCodeWithLimiteTime(int userId)
        {
            var codes = await _unitOfWork.Discounts.GetPrivateDiscountCodesWithLessThanOneWeekLeft(userId);

            return codes.Select(code => new PrivateDiscountDetailsDto
            {
                code = code.code
            }).ToList();
        }

        public async Task<GiftDiscountDetailDto> UserGiftedCodeCount(int userId)
        {
            var giftCodes = await _unitOfWork.Discounts.GetGiftedDiscountCodesCount(userId);

            return new GiftDiscountDetailDto
            {
                gifted_code = giftCodes
            };
        }

        public DiscountSummaryDto UserDiscountsSummary(IEnumerable<PrivateDiscountDetailsDto> privateCodes, GiftDiscountDetailDto giftCodes)
        {
            return new DiscountSummaryDto
            {
                discounts = privateCodes,
                gifts = giftCodes
            };
        }
        
        public async Task<IEnumerable<ShoppingCartsDetailsDto>> UserRecentPurchases(int userId)
        {
            var shoppingCarts = await _unitOfWork.ShoppingCarts.UserRecentPurchasesAsync(userId);

            if (!shoppingCarts.Any())
            {
                return new List<ShoppingCartsDetailsDto>
                {
                    new ShoppingCartsDetailsDto
                    {
                        cart_number      = 0,
                        cart_status      = "No Cart",
                        total_items      = 0,
                        total_quantity   = 0,
                        total_cart_price = 0
                    }
                };
            }

            return shoppingCarts.Select(cart => new ShoppingCartsDetailsDto
            {
                cart_number      = cart.cart_number,
                cart_status      = cart.cart_status,
                total_items      = cart.total_items,
                total_quantity   = cart.total_quantity,
                total_cart_price = cart.total_cart_price
            }).ToList();
        }


        public async Task<IEnumerable<CartDetailsDto>> UserCartsStatus(int userId)
        {
            var carts = await _unitOfWork.ShoppingCarts.UserCartsStatus(userId);

            return carts.Select(cart => new CartDetailsDto
            {
                cart_number = cart.cart_number,
                cart_status = cart.cart_status,
                total_items = cart.total_items
            }).ToList();
        }

        public async Task<UserProfitDto> VIPUserProfit(int userId)
        {
            var profit = await _unitOfWork.Transactions.GetUserProfitForVIPClients(userId);

            return new UserProfitDto
            {
                user_profit = profit.user_profit,
            };
        }

        public async Task<bool> isUserVIPChecker(int userId)
        {
            return await _unitOfWork.Users.isUserVIP(userId);
        }
    }
}