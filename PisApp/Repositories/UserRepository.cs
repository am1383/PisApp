using Microsoft.EntityFrameworkCore;
using PisApp.API.Interfaces;
using PisApp.API.Entities.Common;
using PisApp.API.Entities;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class UserRepository(IUnitOfWork unitOfWork) : IUserRepository
    {
        public async Task<bool> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var query  = "SELECT phone_number FROM client WHERE phone_number = @p0";

            var result = await unitOfWork.Context.Set<Login>()
                                                 .FromSqlRaw(query, phoneNumber)
                                                 .FirstOrDefaultAsync();

            return result != default; 
        }

        public async Task<int> GetUserIdByPhoneNumberAsync(string phoneNumber)
        {
            var query = "SELECT client_id FROM client WHERE phone_number = @p0";

            var userId = await unitOfWork.Context.Set<User>()
                                                 .FromSqlRaw(query, phoneNumber)
                                                 .FirstOrDefaultAsync();

            return userId.client_id;
        }

        public async Task<UserDetail> GetUserDetailAsync(int userId)
        {
            var query = @"
                        SELECT 
                            first_name, last_name, wallet_balance, time_stamp, referral_code 
                        FROM 
                            client
                        WHERE 
                            client_id = @p0
                    ";  

            return await unitOfWork.Context.Set<UserDetail>()
                                           .FromSqlRaw(query, userId) 
                                           .FirstOrDefaultAsync();
        }

        public async Task<bool> isUserVIP(int userId)
        {
            var query  = "SELECT EXISTS(SELECT * FROM vip_client WHERE client_id = @p0)";

            var result = await unitOfWork.Context.Set<VIPCheckResult>()
                                                 .FromSqlRaw(query, userId)
                                                 .FirstOrDefaultAsync();

            return result.exists;             
        }

        public async Task<DateTime> VIPChecker(int userId)
        {
            var query  = "SELECT expiration_time FROM vip_client WHERE client_id = @p0";

            var result = await unitOfWork.Context.Set<VIPUser>()
                                                 .FromSqlRaw(query, userId)
                                                 .FirstOrDefaultAsync();

            return result?.expiration_time ?? DateTime.MinValue;
        }

        public async Task<string> GetUserRefferCode(int userId)
        {
            var query  = "SELECT referral_code FROM client WHERE client_id = @p0";

            var result =  await unitOfWork.Context.Set<UserRefferCode>()
                                                  .FromSqlRaw(query, userId)
                                                  .FirstOrDefaultAsync();

            return result.referral_code;
        }
    }
}