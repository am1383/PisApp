namespace PisApp.API.Products.Entities
{
    public class Cpu 
    {
        public required int max_memory_limit   { get; set; }        
        public required int wattage  { get; set; }   
        public required decimal base_frequency  { get; set; }
        public required decimal boost_frequency { get; set; } 
    }
}