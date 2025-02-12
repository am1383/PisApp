using PisApp.API.Exceptions;
using PisApp.API.DTOs;
using PisApp.API.Interface;
using PisApp.API.Interface.UnitOfWork;

namespace PisApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> FindUserIdByPhoneNumber(string phoneNumber)
        {
            if (await _unitOfWork.Users.GetUserByPhoneNumberAsync(phoneNumber))
            {   
                return await _unitOfWork.Users.GetUserId(phoneNumber);
            } else {

                throw new NotFoundExceptions("User");
            }
        }

        public async Task<UserDetailDto> GetUserDetailsById(int userId)
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
                    is_VIP = false, 
                    remaining_time = 0, 
                };
            }

            var remainingTime = vipExpiryDate - DateTime.UtcNow;
            var remainingDays = remainingTime.TotalDays < 0 ? 0 : (int)remainingTime.TotalDays;

            return new VIPUserDetailDto
            {
                is_VIP = true, 
                remaining_time = remainingDays, 
            };
        }

        public async Task<IEnumerable<AddressDetailDto>> GetUserAddressesById(int userId)
        {
            return await _unitOfWork.Addresses.GetAllAddressesById(userId);
        }
    }
}