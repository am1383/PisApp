namespace PisApp.API.Products.Dtos.Common
{
    public class CommonProductsDto
    {
        public required int wattage    { get; set; } 
        public required int product_id { get; set; }     
        public required string brand   { get; set; }
        public required string model   { get; set; }
        public required string category   { get; set; }
        public required int current_price { get; set; }
        public required int stock_count   { get; set; }    
    }
}