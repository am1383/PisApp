using Microsoft.AspNetCore.Mvc;
using PisApp.API.Dtos.LoginDto;
using PisApp.API.Interfaces;
using PisApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using PisApp.API.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly JwtService _jwtService;

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService  = jwtService;
        }

        [HttpPost("login")]
        public async Task<ResponseDto<string>> Login(LoginDto loginDto)
        {
            try 
            {
                var phoneNumber = PhoneNumberHelper.Normalize(loginDto.phone_number);

                var userId      = await _userService.FindUserIdByPhoneNumber(phoneNumber);

                var isUserVIP   = await _userService.isUserVIPChecker(userId);

                var token       = _jwtService.GenerateToken(userId, isUserVIP);

                return new ResponseDto<string>(true, token);  
            } 
            catch (Exception e)
            {
                return new ResponseDto<string>(false, default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ResponseDto<UserDetailDto>> Show()
        {
            try
            {
                var userId          = _jwtService.GetUserId(HttpContext);

                var user            = await _userService.GetUserDetailsById(userId);

                var isUserVip       = await _userService.GetRemainingTimeForVIP(userId);

                var countUserReffer = await _userService.CountUserRefferer(user.referral_code);

                var userDetail      = _userService.Details(user, isUserVip, countUserReffer);

                return new ResponseDto<UserDetailDto>(true, userDetail);
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDetailDto>(false, default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("addresses")]
        public async Task<ResponseDto<IEnumerable<AddressDetailDto>>> Addresses(AuthorizationFilterContext context)
        {
            try
            {
                var userId    = _jwtService.GetUserId(HttpContext);

                var addresses = await _userService.GetUserAddressesById(userId);

                return new ResponseDto<IEnumerable<AddressDetailDto>>(true, addresses);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<AddressDetailDto>>(false, default!, $"Exception : {e.Message}");
            }
        }

        [Authorize]
        [HttpGet("code")]
        public async Task<ResponseDto<DiscountSummaryDto>> DiscountCodeStatus(AuthorizationFilterContext context)
        {
            try
            {
                var userId           = _jwtService.GetUserId(HttpContext);

                var privateCode      = await _userService.UserPrivateCodeWithLimiteTime(userId);

                var giftedCode       = await _userService.UserGiftedCodeCount(userId);

                var discountsSummary = _userService.UserDiscountsSummary(privateCode, giftedCode);

                return new ResponseDto<DiscountSummaryDto>(true, discountsSummary);
            }
            catch (Exception e)
            {
                return new ResponseDto<DiscountSummaryDto>(false, default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("purchases")]
        public async Task<ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>> RecentPurchases(AuthorizationFilterContext context)
        {
            try
            {
                var userId        = _jwtService.GetUserId(HttpContext);

                var shoppingCarts = await _userService.UserRecentPurchases(userId);

                return new ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>(true, shoppingCarts);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<ShoppingCartsDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("carts/status")]
        public async Task<ResponseDto<IEnumerable<CartDetailsDto>>> CartsStatus(AuthorizationFilterContext context)
        {
            try
            {
                var userId = _jwtService.GetUserId(HttpContext);

                var carts  = await _userService.UserCartsStatus(userId);

                return new ResponseDto<IEnumerable<CartDetailsDto>>(true, carts);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<CartDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [Authorize]
        [HttpGet("profit")]
        public async Task<ResponseDto<UserProfitDto>> VIPUserProfit(AuthorizationFilterContext context)
        {
            try
            {
                var userId = _jwtService.GetUserId(HttpContext);

                var profit = await _userService.VIPUserProfit(userId);

                return new ResponseDto<UserProfitDto>(true, profit);
            }
            catch (Exception e)
            {
                return new ResponseDto<UserProfitDto>(false, default!, $"{e.Message}");
            }
        }
    }
}