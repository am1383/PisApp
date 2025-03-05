using PisApp.API.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Dtos
{
    public class ShoppingCartsDetailsDto
    {
        public required List<CartItemProduct> products { get; set; }

        public required decimal total_price { get; set; }
    }
}