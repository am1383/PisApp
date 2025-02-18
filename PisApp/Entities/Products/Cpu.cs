using PisApp.API.Products.Entities.Common;

namespace PisApp.API.Products.Entities
{
    public class Cpu : CommonProductsEntity
    {
        public required int max_memory_limit { get; set; }        
        public required int  wattage      { get; set; }   
        public required string generation { get; set; }
        public required string microarchitecture { get; set; }
        public required int num_cores   { get; set; }
        public required int num_threads { get; set; }
        public required decimal base_frequency  { get; set; }
        public required decimal boost_frequency { get; set; } 
    }
}