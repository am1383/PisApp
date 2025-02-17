using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Entities;
using PisApp.API.Interfaces;

namespace PisApp.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly PisAppDbContext _context;

        public DiscountRepository(PisAppDbContext context)
        {   
            _context = context;
        }

        public async Task<List<PrivateDiscount>> GetPrivateDiscountCodesWithLessThanOneWeekLeft(int userId)
        {
            var query = @"
                SELECT 
                    pc.code  
                FROM 
                    private_code pc
                JOIN 
                    discount_code dc ON pc.code = dc.code
                WHERE 
                    pc.client_id = @p0
                    AND dc.expiration_time <= NOW() + INTERVAL '7 days'
            ";

            return await _context.Set<PrivateDiscount>()
                                 .FromSqlRaw(query, userId) 
                                 .ToListAsync();
        }

        public async Task<int> GetGiftedDiscountCodesCount(int userId)
        {
            var query = @"
                SELECT 
                    COUNT(*) 
                FROM 
                    private_code pc
                JOIN 
                    discount_code dc ON pc.code = dc.code
                WHERE
                    pc.client_id = @p0
                    AND dc.code_type = 'private'
                    AND dc.expiration_time > NOW()";

            var result = await _context.Set<Discount>()
                                       .FromSqlRaw(query, userId)
                                       .FirstOrDefaultAsync();

            return result.count;
        }
    }
}