using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Dtos
{
    public class ShoppingCartsDetailsDto
    {
        public required List<Product> products { get; set; }

        public required decimal total_price { get; set; }
    }
}