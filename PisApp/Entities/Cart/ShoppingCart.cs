namespace PisApp.API.Entities
{
    public class ShoppingCart
    {        
        public required int locked_number   { get; set; }

        public required DateTime time_stamp { get; set; }
    }
}