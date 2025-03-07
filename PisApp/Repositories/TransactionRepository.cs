using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class TransactionRepository(IUnitOfWork unitOfWork) : ITransactionRepository
    {
        public async Task<UserProfit> GetUserProfitForVIPClients(int userId)
        {
            var query = @"
                    SELECT COALESCE(SUM(adt.cart_price) * 0.15, 0) AS user_profit
                    FROM vip_client vc
                    JOIN issued_for ifo ON vc.client_id = ifo.client_id
                    JOIN transaction t  ON ifo.tracking_code = t.tracking_code
                    JOIN added_to adt   ON ifo.client_id = adt.client_id 
                        AND ifo.cart_number   = adt.cart_number 
                        AND ifo.locked_number = adt.locked_number
                    WHERE vc.client_id        = @p0
                        AND t.transaction_status = 'successful'
                        AND t.time_stamp        >= DATE_TRUNC('month', CURRENT_DATE)
                        AND t.time_stamp        <= NOW()
                        AND vc.expiration_time  >= t.time_stamp
                    ";

            return await unitOfWork.Context.Set<UserProfit>()
                                           .FromSqlRaw(query, userId)
                                           .FirstOrDefaultAsync();
        }
    }
}