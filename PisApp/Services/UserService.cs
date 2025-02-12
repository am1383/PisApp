using Microsoft.AspNetCore.Http.HttpResults;
using MRH.Backend.Identity.API.Exceptions;
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
    }
}