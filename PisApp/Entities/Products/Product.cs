namespace PisApp.API.Products.Entities.Common
{
    public class Product
    {
        public required string brand   { get; set; }
        public required string model   { get; set; }
        public required string category   { get; set; }
        public required int current_price { get; set; }
        public required int stock_count   { get; set; }
    }
}