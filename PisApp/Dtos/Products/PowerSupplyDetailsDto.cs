namespace PisApp.API.Products.Entities
{
    public class PowerSupplyDetailsDto
    {
        public required int product_id { get; set; }         
        public required int supported_wattage { get; set; }
        public required decimal depth  { get; set; }
        public required decimal height { get; set; }
        public required decimal width  { get; set; }
        public required string brand   { get; set; }
        public required string model   { get; set; }
        public required string category   { get; set; }
        public required int current_price { get; set; }
        public required int stock_count   { get; set; }       
    }
}