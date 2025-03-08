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
            var query = @"SELECT d.code
                          FROM discount_code d
                          INNER JOIN private_code p ON d.code = p.code
                          WHERE p.client_id     = @p0
                          AND d.expiration_time > NOW()
                          AND d.expiration_time < (NOW() + INTERVAL '7 DAY')
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
                            referrer_id
                        FROM refers
                        WHERE referrer_id = @p0

                        UNION ALL

                        SELECT
                            r.referee_id,
                            r.referrer_id
                        FROM referral_chain rc
                        JOIN refers r ON rc.referee_id = r.referrer_id
                    )
                    SELECT 
                        COUNT(*) AS gifted_codes_count
                    FROM referral_chain
                ";

            var result = await unitOfWork.Context.Set<Discount>()
                                                 .FromSqlRaw(query, userRefferCode)
                                                 .FirstOrDefaultAsync();

            return result.gifted_codes_count;
        }
    }
}