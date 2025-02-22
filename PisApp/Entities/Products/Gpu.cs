using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Products.Entities
{
    public class Gpu : CommonProductsEntity
    {
        public required int ram_size   { get; set; }     
        public required int wattage    { get; set; }
        public required int  num_fans  { get; set; }
        public required decimal clock_speed { get; set; }
        public required decimal depth       { get; set; }
        public required decimal height      { get; set; } 
        public required decimal width       { get; set; }
    }
}