using Microsoft.AspNetCore.Mvc;
using PisApp.API.DTOs.LoginDto;
using PisApp.API.DTOs;
using PisApp.API.Interface;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ResponseDTO<> Login(LoginDto loginDto)
        {

        }
    }
}