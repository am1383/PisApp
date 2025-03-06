using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Products.Dtos
{
    public class CoolerDetailsDto : CommonProductsDto
    {
        public required string cooling_method    { get; set; }
        public required int fan_size             { get; set; }             
        public required int max_rotational_speed { get; set; }
        public required decimal depth  { get; set; } 
        public required decimal height { get; set; }  
        public required decimal width  { get; set; }
    }
}