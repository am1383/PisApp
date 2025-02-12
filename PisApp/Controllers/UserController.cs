using Microsoft.AspNetCore.Mvc;
using PisApp.API.DTOs.LoginDto;
using PisApp.API.Interface;
using PisApp.API.DTOs;

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
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ResponseDto<string>> UserLogin(LoginDto loginDto)
        {
            try 
            {
                var phoneNumber = loginDto.phone_number;

                var userId = await _userService.FindUserIdByPhoneNumber(phoneNumber);

                var token = _jwtService.GenerateToken(userId);

                return new ResponseDto<string>(true, token, null, null);  
            } 
            catch (Exception e)
            {
                return new ResponseDto<string>(false, null, $"Exception : {e.Message}", null);
            }
        }
    }
}