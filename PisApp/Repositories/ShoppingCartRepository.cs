using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Repositories
{
    public class ShoppingCartRepository(IUnitOfWork unitOfWork) : IShoppingCartRepository
    {
        public async Task<List<ShoppingCart>> GetRecentPurchasesNumberAsync(int userId)
        {
            var query = @"
                    SELECT DISTINCT lsc.locked_number, lsc.time_stamp
                    FROM issued_for i
                    JOIN locked_shopping_cart lsc 
                        ON i.client_id      = lsc.client_id 
                        AND i.cart_number   = lsc.cart_number 
                        AND i.locked_number = lsc.locked_number
                    WHERE i.client_id       = @p0
                    ORDER BY lsc.time_stamp DESC
                    LIMIT 5;
            ";

            return await unitOfWork.Context.Set<ShoppingCart>()
                                           .FromSqlRaw(query, userId)
                                           .ToListAsync();
        }

        public async Task<List<Cart>> GetUserCarts(int userId)
        {
            var query = @"SELECT cart_number, cart_status
                          FROM shopping_cart
                          WHERE client_id = @p0
                    ";

            return await unitOfWork.Context.Set<Cart>()
                                           .FromSqlRaw(query, userId)
                                           .ToListAsync();
        }

        public async Task<int> AvailabeUserCarts(int userId)
        {
            var query = @"
                        SELECT COALESCE(COUNT(*), 0) AS count
                        FROM shopping_cart
                        WHERE client_id = @p0 
                            AND cart_status IN ('active', 'locked')
                    ";

            var result = await unitOfWork.Context.Set<Refer>()
                                                 .FromSqlRaw(query, userId)
                                                 .FirstOrDefaultAsync();
            return result.count;
        }

        public async Task<decimal> GetCartItemTotalPrice(int lockedNumber)
        {
            var query  = @"
                        SELECT lsc.locked_number, SUM(a.cart_price * a.quantity) AS total_price
                        FROM issued_for i
                        JOIN locked_shopping_cart lsc 
                            ON i.client_id      = lsc.client_id 
                            AND i.cart_number   = lsc.cart_number 
                            AND i.locked_number = lsc.locked_number
                        JOIN added_to a 
                            ON lsc.client_id      = a.client_id 
                            AND lsc.cart_number   = a.cart_number 
                            AND lsc.locked_number = a.locked_number
                        WHERE lsc.locked_number   = @p0  
                        GROUP BY lsc.locked_number, lsc.time_stamp
                        ORDER BY lsc.time_stamp DESC
                        LIMIT 5
                    ";

            var result = await unitOfWork.Context.Set<CartItem>()
                                                 .FromSqlRaw(query, lockedNumber)
                                                 .FirstOrDefaultAsync();
            return result.total_price;
        }
    }
}