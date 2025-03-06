namespace PisApp.API.Products.Entities.Common
{
    public class CommonProduct
    {
        public required int product_id  { get; set; }   
        public required string brand    { get; set; }
        public required string model    { get; set; }
    }
}