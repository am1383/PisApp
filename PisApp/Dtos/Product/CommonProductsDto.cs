namespace PisApp.API.Products.Dtos.Common
{
    public class CommonProductsDto
    {
        public required int product_id { get; set; }     
        public required string brand   { get; set; }
        public required string model   { get; set; }
    }
}