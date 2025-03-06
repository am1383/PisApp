using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PisApp.API.Dtos.LoginDto;
using PisApp.API.Interfaces;
using PisApp.API.Dtos;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController(IUserService userService, JwtService jwtService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<ResponseDto<string>> Login(LoginDto loginDto)
        {
            try 
            {
                var userId      = await userService.FindUserIdByPhoneNumber(loginDto.phone_number);

                var isUserVIP   = await userService.isUserVIPChecker(userId);

                var token       = jwtService.GenerateToken(userId, isUserVIP);

                return new ResponseDto<string>(token);  
            } 
            catch (Exception e)
            {
                return new ResponseDto<string>(default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ResponseDto<UserDetailDto>> Show()
        {
            try
            {
                var userId          = jwtService.GetUserId(HttpContext);

                var user            = await userService.GetUserDetailsById(userId);

                var isUserVip       = await userService.GetVIPUserDetails(userId);

                var countUserReffer = await userService.CountUserRefferer(user.referral_code);

                var userDetail      = userService.MapUserDetails(user, isUserVip, countUserReffer);

                return new ResponseDto<UserDetailDto>(userDetail);
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDetailDto>(default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("addresses")]
        public async Task<ResponseDto<IEnumerable<AddressDetailDto>>> Addresses()
        {
            try
            {
                var userId    = jwtService.GetUserId(HttpContext);

                var addresses = await userService.GetUserAddressesById(userId);

                return new ResponseDto<IEnumerable<AddressDetailDto>>(addresses);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<AddressDetailDto>>(default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("code")]
        public async Task<ResponseDto<DiscountSummaryDto>> DiscountCodeStatus()
        {
            try
            {
                var userId           = jwtService.GetUserId(HttpContext);

                var privateCode      = await userService.UserPrivateCodeWithLimiteTime(userId);

                var userRefferCode   = await userService.userRefferCode(userId);

                var giftedCodeCount  = await userService.UserGiftedCodeCount(userRefferCode);

                var discountsSummary = userService.MapUserDiscountsSummary(privateCode, giftedCodeCount);

                return new ResponseDto<DiscountSummaryDto>(discountsSummary);
            }
            catch (Exception e)
            {
                return new ResponseDto<DiscountSummaryDto>(default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("purchases")]
        public async Task<ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>> RecentPurchases()
        {
            try
            {
                var userId        = jwtService.GetUserId(HttpContext);

                var shoppingCarts = await userService.UserRecentPurchases(userId);

                return new ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>(shoppingCarts);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>(default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("carts/status")]
        public async Task<ResponseDto<CartResponseDto>> CartsStatus()
        {
            try
            {
                var userId = jwtService.GetUserId(HttpContext);

                var carts  = await userService.UserCartsStatus(userId);

                return new ResponseDto<CartResponseDto>(carts);
            }
            catch (Exception e)
            {
                return new ResponseDto<CartResponseDto>(default!, $"{e.Message}");
            }
        }
    }
}