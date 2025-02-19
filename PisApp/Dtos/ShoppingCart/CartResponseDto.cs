namespace PisApp.API.Dtos
{
    public class CartResponseDto
    {
        public List<CartDetailsDto> carts { get; set; }
        public int available_carts        { get; set; } 
    }
}   