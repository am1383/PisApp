using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class ShoppingCartRepository(IUnitOfWork unitOfWork) : IShoppingCartRepository
    {
        public async Task<List<ShoppingCart>> UserRecentPurchasesAsync(int userId)
        {
            var query = @"
                        SELECT
                            s.cart_number,
                            s.cart_status,
                            COUNT(a.product_id) AS total_items,
                            SUM(a.quantity) AS total_quantity,
                            SUM(a.cart_price) AS total_cart_price
                        FROM
                            shopping_cart s
                        LEFT JOIN
                            added_to a ON s.cart_number = a.cart_number AND s.client_id = a.client_id
                        WHERE
                            s.client_id = @p0
                        GROUP BY
                            s.cart_number, s.cart_status  
                        ORDER BY
                            s.cart_number DESC  
                        LIMIT 5;
                ";

            return await unitOfWork.Context.Set<ShoppingCart>()
                                           .FromSqlRaw(query, userId)
                                           .ToListAsync();
        }

        public async Task<List<Cart>> UserCartsStatus(int userId)
        {
            var query = @"
                SELECT
                    s.cart_number,
                    s.cart_status,
                    COUNT(a.product_id) AS total_items
                FROM
                    shopping_cart s
                LEFT JOIN
                    added_to a ON s.cart_number = a.cart_number AND s.client_id = a.client_id
                WHERE
                    s.client_id = @p0 
                GROUP BY
                    s.cart_number, s.cart_status;
                ";

            return await unitOfWork.Context.Set<Cart>()
                                           .FromSqlRaw(query, userId)
                                           .ToListAsync();
        }

        public async Task<int> AvailabeUserCarts(int userId)
        {
            var query = @"
                        SELECT
                            COUNT(*)
                        FROM 
                            shopping_cart
                        WHERE 
                            client_id = @p0 
                            AND cart_status IN ('active', 'locked')
                        ";

            var result = await unitOfWork.Context.Set<Refer>()
                                                 .FromSqlRaw(query, userId)
                                                 .FirstOrDefaultAsync();
            return result.count;
        }
    }
}