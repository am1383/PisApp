using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfit> GetUserProfitForVIPClients(int userId)
        {
            var query = @"
                SELECT COALESCE(SUM(at.quantity * at.cart_price), 0) * 0.15 AS user_profit
                    FROM issued_for AS ifr
                    JOIN transaction AS t 
                        ON ifr.tracking_code = t.tracking_code
                    JOIN added_to AS at 
                        ON at.client_id      = ifr.client_id
                        AND at.cart_number   = ifr.cart_number
                        AND at.locked_number = ifr.locked_number
                    WHERE t.time_stamp      >= date_trunc('month', CURRENT_DATE)
                        AND t.time_stamp    <  date_trunc('month', CURRENT_DATE + INTERVAL '1 month')
                        AND ifr.client_id    = @p0";

            return await _unitOfWork.Context.Set<UserProfit>()
                                            .FromSqlRaw(query, userId)
                                            .FirstOrDefaultAsync();
        }
    }
}