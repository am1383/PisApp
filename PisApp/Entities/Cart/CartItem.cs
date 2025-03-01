namespace PisApp.API.Products.Entities.Common
{
    public class CartItem
    {
        public required decimal total_price { get; set; }

        public required int locked_number   { get; set; }
    }
}