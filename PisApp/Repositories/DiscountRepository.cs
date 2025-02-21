using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class DiscountRepository(IUnitOfWork unitOfWork) : IDiscountRepository
    {
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

            return await unitOfWork.Context.Set<PrivateDiscount>()
                                           .FromSqlRaw(query, userId) 
                                           .ToListAsync();
        }

        public async Task<int> GetGiftedDiscountCodesCount(string userRefferCode)
        {
            var query = @"
                    WITH RECURSIVE referral_chain AS (
                    SELECT
                        referee_id,         
                        referrer_id,
                        1 AS level,         
                        referee_id AS origin
                    FROM refers
                    
                    UNION ALL

                    SELECT
                        r.referee_id,
                        r.referrer_id,
                        rc.level + 1 AS level,
                        rc.origin
                    FROM referral_chain rc
                    JOIN refers r ON rc.referrer_id = r.referee_id
                )
                SELECT 
                    origin,
                    (MAX(level) + 1) AS gifted_codes_count  
                FROM referral_chain
                GROUP BY origin
                ";

            var result = await unitOfWork.Context.Set<Discount>()
                                                 .FromSqlRaw(query, userRefferCode)
                                                 .FirstOrDefaultAsync();

            return result.count;
        }
    }
}