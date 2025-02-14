namespace PisApp.API.Products.Dtos
{
    public class CoolerDetailsDto
    {
        public required string cooling_method { get; set; }
        public required int fan_size { get; set; }             
        public required int max_rotational_speed { get; set; }
        public required int wattage { get; set; }           
        public required decimal depth { get; set; } 
        public required decimal height { get; set; }  
        public required decimal width  { get; set; }
    }
}