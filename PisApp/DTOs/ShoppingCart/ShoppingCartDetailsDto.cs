namespace PisApp.API.DTOs
{
    public class ShoppingCartsDetailsDto
    {
        public required int cart_number { get; set; }

        public required string cart_status { get; set; }

        public required int total_items { get; set; }

        public required int total_quantity { get; set; }

        public required int total_cart_price { get; set; }
    }
}