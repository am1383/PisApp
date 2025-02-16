using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Interfaces;
using PisApp.API.Entities.Common;
using PisApp.API.Entities;

namespace PisApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PisAppDbContext _context;

        public UserRepository(PisAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var query = "SELECT phone_number FROM client WHERE phone_number = @p0";

            var result = await _context.Set<BaseEntity>()
                                       .FromSqlRaw(query, phoneNumber)
                                       .FirstOrDefaultAsync();

            return result != null; 
        }

        public async Task<int> GetUserId(string phoneNumber)
        {
            var query = "SELECT client_id FROM client WHERE phone_number = @p0";

            var userId = await _context.Set<User>().FromSqlRaw(query, phoneNumber)
                                                   .FirstOrDefaultAsync();

            return userId.client_id;
        }

        public async Task<UserDetail> GetUserDetailAsync(int userId)
        {
            var query = "SELECT first_name, last_name, wallet_balance, time_stamp, referral_code " +
                        "FROM client WHERE client_id = @p0";  

            return await _context.Set<UserDetail>()
                                 .FromSqlRaw(query, userId) 
                                 .FirstOrDefaultAsync();
        }

        public async Task<DateTime> VIPChecker(int userId)
        {
            var query = "SELECT expiration_time FROM vip_client WHERE client_id = @p0";

            var result = await _context.Set<VIPUser>()
                                       .FromSqlRaw(query, userId)
                                       .FirstOrDefaultAsync();

            return result?.expiration_time ?? DateTime.MinValue;
        }
    }
}