namespace PisApp.API.Entities
{
    public class CartItemProduct
    {
        public required string brand   { get; set; }
        public required string model   { get; set; }
        public required string category   { get; set; }
        public required int price      { get; set; }
        public required int quantity   { get; set; }
    }
}