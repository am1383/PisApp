using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Products.Entities
{
    public class GpuDetailsDto : CommonProductsDto
    {
        public required int ram_size   { get; set; }     
        public required int  num_fans  { get; set; }
        public required decimal clock_speed { get; set; }
        public required decimal depth  { get; set; }
        public required decimal height { get; set; } 
        public required decimal width  { get; set; }   
    }
}