using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Products.Dtos
{
    public class RamDetailDto : CommonProductsDto
    {
        public required string generation { get; set; }
        public required decimal capacity  { get; set; }  
        public required decimal frequency { get; set; } 
        public required decimal depth     { get; set; }
        public required decimal height    { get; set; }  
        public required decimal width     { get; set; }  
    }
}