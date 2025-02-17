using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Products.Dtos
{
    public class CpuDetailsDto : CommonProductsDto
    {
        public required int max_memory_limit     { get; set; }        
        public required string generation        { get; set; }
        public required string microarchitecture { get; set; }
        public required int num_cores   { get; set; }
        public required int num_threads { get; set; }
        public required decimal base_frequency  { get; set; }
        public required decimal boost_frequency { get; set; } 
    }
}