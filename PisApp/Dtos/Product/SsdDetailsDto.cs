using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Products.Dtos
{
    public class SsdDetailDto : CommonProductsDto
    {
        public required decimal capacity { get; set; }   
    }
}