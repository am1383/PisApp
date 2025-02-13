namespace PisApp.API.Dtos
{
    public class CartDetailsDto
    {
        public required int cart_number { get; set; }

        public required string cart_status { get; set; }

        public required int total_items { get; set; }
    }
}