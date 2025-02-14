using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Entities;
using PisApp.API.Interfaces;

namespace PisApp.API.Repositories
{
    public class ReferRepository : IReferRepository
    {
        private readonly PisAppDbContext _context;

        public ReferRepository(PisAppDbContext context)
        {   
            _context = context;
        }

        public async Task<int> CountUserReferrerByCode(string referCode)
        {
            var query = @"SELECT COUNT(*) FROM refers WHERE referrer_id = {0}";
                
            var result = await _context.Set<Refer>()
                                    .FromSqlRaw(query, referCode)
                                    .FirstOrDefaultAsync();

            return result.count;
        }
    }
}