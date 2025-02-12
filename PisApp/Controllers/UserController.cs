using Microsoft.AspNetCore.Mvc;
using PisApp.API.DTOs.LoginDto;
using PisApp.API.Interface;
using PisApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ResponseDto<string>> UserLogin(LoginDto loginDto)
        {
            try 
            {
                var phoneNumber = loginDto.phone_number;

                var userId = await _userService.FindUserIdByPhoneNumber(phoneNumber);

                var token  = _jwtService.GenerateToken(userId);

                return new ResponseDto<string>(true, token, null, null);  
            } 
            catch (Exception e)
            {
                return new ResponseDto<string>(false, null, $"Exception : {e.Message}", null);
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ResponseDto<UserDetailDto>> UserDetails()
        {
            try
            {
                var userId = _jwtService.GetUserId(Request);

                var user   = await _userService.GetUserDetailsById(userId);

                var isVip  = await _userService.GetRemainingTimeForVIP(userId);

                var userDetail = new UserDetailDto {
                    first_name = user.first_name,
                    last_name = user.last_name, 
                    wallet_balance = user.wallet_balance,
                    referral_code = user.referral_code,
                    time_stamp = user.time_stamp,
                    vip_detail = isVip,
                };

                return new ResponseDto<UserDetailDto>(true, userDetail, null, null);
            }
            catch(Exception e)
            {
                return new ResponseDto<UserDetailDto>(false, null, $"Exception : {e.Message}", null);
            }
        }

        [Authorize]
        [HttpGet("addresses")]
        public async Task<ResponseDto<IEnumerable<AddressDetailDto>>> UserAddresses()
        {
            var userId = _jwtService.GetUserId(Request);

            var addresses = await _userService.GetUserAddressesById(userId);

            return new ResponseDto<IEnumerable<AddressDetailDto>>(true, addresses, null, null);
        }
    }
}