namespace PisApp.API.Dtos
{
    public class MotherboardDetailsDto
    {
        public required int wattage { get; set; }
        public required string chipset_name { get; set; }
        public required int num_memory_slots { get; set; }
        public required decimal memory_speed_range { get; set; }
        public required decimal depth  { get; set; }
        public required decimal height { get; set; }  
        public required decimal width  { get; set;}   
    }
}