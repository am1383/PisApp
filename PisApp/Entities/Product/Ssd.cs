using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Products.Entities
{
    public class Ssd : CommonProduct
    {
        public required int wattage      { get; set; }
        public required decimal capacity { get; set; }
    }
}