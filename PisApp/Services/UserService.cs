using PisApp.API.Exceptions;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Entities;
using Microsoft.AspNetCore.Mvc.Razor;
using PisApp.API.Utils;

namespace PisApp.API.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        public UserDetailDto MapUserDetails(UserDetail user, VIPUserDetailDto isUserVIP, int countUserReffer)
        {
            return new UserDetailDto 
            {
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
            var normalizePhoneNumber = PhoneNumberHelper.NormalizePhoneNumber(phoneNumber);

            if (await unitOfWork.Users.GetUserByPhoneNumberAsync(normalizePhoneNumber))
            {   
                return await unitOfWork.Users.GetUserIdByPhoneNumberAsync(normalizePhoneNumber);
            } 
            
            throw new UserNotFoundExceptions();
        }

        public async Task<UserDetail> GetUserDetailsById(int userId)
        {
            return await unitOfWork.Users.GetUserDetailAsync(userId);
        }

        public async Task<VIPUserDetailDto> GetRemainingTimeForVIP(int userId)
        {
            var vipExpiryDate = await unitOfWork.Users.VIPChecker(userId);
            
            if (vipExpiryDate < DateTime.UtcNow)
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
            var addresses = await unitOfWork.Addresses.GetAllAddressesById(userId);

            return addresses.Select(a => new AddressDetailDto
            {
                province       = a.province,
                remain_address = a.remain_address,
            });
        }

        public async Task<int> CountUserRefferer(string referCode)
        {
            return await unitOfWork.Refers.CountUserReferrerByCode(referCode);
        }

        public async Task<IEnumerable<PrivateDiscountDetailsDto>> UserPrivateCodeWithLimiteTime(int userId)
        {
            var codes = await unitOfWork.Discounts.GetPrivateDiscountCodesWithLessThanOneWeekLeft(userId);

            return codes.Select(code => new PrivateDiscountDetailsDto
            {
                code = code.code
            }).ToList();
        }

        public async Task<GiftDiscountDetailDto> UserGiftedCodeCount(string userRefferCode)
        {
            var giftedCodes = await unitOfWork.Discounts.GetGiftedDiscountCodesCount(userRefferCode);

            return new GiftDiscountDetailDto
            {
                gifted_code = giftedCodes
            };
        }

        public DiscountSummaryDto UserDiscountsSummary(IEnumerable<PrivateDiscountDetailsDto> privateCodes, GiftDiscountDetailDto giftCodes)
        {
            return new DiscountSummaryDto
            {
                discounts = privateCodes,
                gifts     = giftCodes
            };
        }
        
        public async Task<IEnumerable<ShoppingCartsDetailsDto>> UserRecentPurchases(int userId)
        {
            var shoppingCarts = await unitOfWork.ShoppingCarts.UserRecentPurchasesAsync(userId);

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

        public async Task<CartResponseDto> UserCartsStatus(int userId)
        {
            var carts          = await unitOfWork.ShoppingCarts.UserCartsStatus(userId);

            var availableCarts = await unitOfWork.ShoppingCarts.AvailabeUserCarts(userId);

            return new CartResponseDto
            {
                carts = carts.Select(cart => new CartDetailsDto
                {
                    cart_number = cart.cart_number,
                    cart_status = cart.cart_status,
                    total_items = cart.total_items
                }).ToList(),
                available_carts = availableCarts
            };
        }

        public async Task<UserProfitDto> VIPUserProfit(int userId)
        {
            var profit = await unitOfWork.Transactions.GetUserProfitForVIPClients(userId);

            return new UserProfitDto
            {
                user_profit = profit.user_profit,
            };
        }

        public async Task<bool> isUserVIPChecker(int userId)
        {
            return await unitOfWork.Users.isUserVIP(userId);
        }

        public async Task<string> userRefferCode(int userId)
        {
            return await unitOfWork.Users.GetUserRefferCode(userId);
        }
    }
}