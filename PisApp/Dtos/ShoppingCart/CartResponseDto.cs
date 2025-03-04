namespace PisApp.API.Dtos
{
    public class CartResponseDto
    {
        public required List<CartDetailsDto> carts { get; set; }
        public required int available_carts { get; set; } 
    }
}   