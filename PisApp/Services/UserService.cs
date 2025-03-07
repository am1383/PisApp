using PisApp.API.Exceptions;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Entities;
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

            if (await unitOfWork.Users.FindUserByPhoneNumberOrFailAsync(normalizePhoneNumber) is true)
            {   
                return await unitOfWork.Users.GetUserIdByPhoneNumberAsync(normalizePhoneNumber);
            } 
            
            throw new UserNotFoundExceptions();
        }

        public async Task<UserDetail> GetUserDetailsById(int userId)
        {
            return await unitOfWork.Users.GetUserDetailAsync(userId);
        }

        public async Task<VIPUserDetailDto> GetVIPUserDetails(int userId)
        {
            var vipExpiryDate = await unitOfWork.Users.VIPChecker(userId);
            
            if (vipExpiryDate < DateTime.UtcNow)
            {
                return new VIPUserDetailDto
                {
                    is_VIP         = false, 
                    profit         = 0,
                    remaining_time = 0, 
                };
            }

            var remainingDays = VIPRemainingDaysCalculator(vipExpiryDate);
            var profit        = await VIPUserProfit(userId);

            return new VIPUserDetailDto
            {
                is_VIP         = true, 
                profit         = profit,
                remaining_time = remainingDays, 
            };
        }

        private async Task<decimal> VIPUserProfit(int userId)
        {
            var profit = await unitOfWork.Transactions
                                         .GetUserProfitForVIPClients(userId);

            return profit.user_profit;
        }

        private int VIPRemainingDaysCalculator(DateTime vipExpireDate)
        {
            var remainingTime = vipExpireDate - DateTime.UtcNow;
            
            return remainingTime.TotalDays < 0 ? 0 : (int)remainingTime.TotalDays;
        }

        public async Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId)
        {
            var addresses = await unitOfWork.Addresses.GetUserAddressesAsync(userId);

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

        public async Task<IEnumerable<PrivateDiscountDetailDto>> UserPrivateCodeWithLimiteTime(int userId)
        {
            var privateCodes = await unitOfWork.Discounts.GetPrivateDiscountCodesWithLessThanOneWeekLeft(userId);

            return privateCodes.Select(code => new PrivateDiscountDetailDto
            {
                code = code.code
            }).ToList();
        }

        public async Task<GiftedDiscountDetailDto> UserGiftedCodeCount(string userRefferCode)
        {
            var giftedCodesCount = await unitOfWork.Discounts.GetGiftedDiscountCodesCount(userRefferCode);

            return new GiftedDiscountDetailDto
            {
                gifted_code = giftedCodesCount
            };
        }

        public DiscountSummaryDto MapUserDiscountsSummary(IEnumerable<PrivateDiscountDetailDto> privateCodes, GiftedDiscountDetailDto giftCodes)
        {
            return new DiscountSummaryDto
            {
                discounts = privateCodes,
                gifts     = giftCodes
            };
        }
        
        public async Task<IEnumerable<ShoppingCartsDetailsDto>> UserRecentPurchases(int userId)
        {
            var lockedNumbers = await unitOfWork.ShoppingCarts.GetRecentPurchasesNumberAsync(userId);

            if (lockedNumbers.Any() is false)
            {
                return new List<ShoppingCartsDetailsDto>
                {
                    new ShoppingCartsDetailsDto
                    {
                        products    = new List<CartItemProduct>(),
                        total_price = 0
                    }
                };
            }

            var uniqueLockerNumbers = lockedNumbers.Select(x => x.locked_number).Distinct().ToList();

            var result = new List<ShoppingCartsDetailsDto>();

            foreach (var lockedNumber in uniqueLockerNumbers)
            {
                var products   = await unitOfWork.Products.GetCartItemProducts(lockedNumber);
                var totalPrice = await unitOfWork.ShoppingCarts.GetCartItemTotalPrice(lockedNumber);

                result.Add(new ShoppingCartsDetailsDto
                {
                    products    = products,
                    total_price = totalPrice
                });
            }

            return result;
        }

        public async Task<CartResponseDto> GetUserCarts(int userId)
        {
            var carts          = await unitOfWork.ShoppingCarts.GetUserCarts(userId);
            var availableCarts = await unitOfWork.ShoppingCarts.AvailabeUserCarts(userId);

            return new CartResponseDto
            {
                carts = carts.Select(cart => new CartDetailsDto
                {
                    cart_number = cart.cart_number,
                    cart_status = cart.cart_status,
                }).ToList(),
            
                available_carts = availableCarts
            };
        }

        public async Task<bool> IsUserVIPChecker(int userId)
        {
            return await unitOfWork.Users.isUserVIP(userId);
        }

        public async Task<string> GetUserRefferCode(int userId)
        {
            return await unitOfWork.Users.GetUserRefferCode(userId);
        }
    }
}