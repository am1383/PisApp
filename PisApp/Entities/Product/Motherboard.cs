namespace PisApp.API.Products.Entities
{
    public class Motherboard
    {
        public required int wattage                { get; set; }
        public required int num_memory_slots       { get; set; }
        public required decimal memory_speed_range { get; set; }
    }
}