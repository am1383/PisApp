namespace PisApp.API.Products.Entities
{
    public class PowerSupply
    {
        public required int supported_wattage { get; set; }
        public required decimal depth { get; set; }
        public required decimal height { get; set; }
        public required decimal width { get; set; }
    }
}