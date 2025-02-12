using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Interface;
using PisApp.API.Entities.Common;

namespace PisApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PisAppDb _context;

        public UserRepository(PisAppDb context)
        {
            _context = context;
        }

        public async Task<bool> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var query = "SELECT phone_number FROM client WHERE phone_number = {0}";

            var result = await _context.Set<BaseEntity>()
                .FromSqlRaw(query, phoneNumber)
                .FirstOrDefaultAsync();

            return result != null; 
        }

        public async Task<int> GetUserId(string phoneNumber)
        {
            var query = "SELECT client_id FROM client WHERE phone_number = {0}";

            var userId = await _context.Set<UserId>().FromSqlRaw(query, phoneNumber)
                .FirstOrDefaultAsync();

            return userId.client_id;
        }
    }
}