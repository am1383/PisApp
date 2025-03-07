namespace PisApp.API.Products.Entities.Common
{
    public class Product
    {
        public required int product_id   { get; set; }
        public required string brand     { get; set; }
        public required string model     { get; set; }
        public required string category  { get; set; }
    }
}