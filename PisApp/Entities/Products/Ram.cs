using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Products.Entities
{
    public class Ram : CommonProductsEntity
    {
        public required int  wattage      { get; set; }
        public required string generation { get; set; }
        public required decimal capacity  { get; set; }  
        public required decimal frequency { get; set; } 
        public required decimal depth  { get; set; }
        public required decimal height { get; set; }  
        public required decimal width  { get; set; } 
    }
}