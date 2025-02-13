namespace PisApp.API.DTOs
{
    public class DiscountSummaryDto
    {
        public IEnumerable<PrivateDiscountDetailsDto>? discounts { get; set; }

        public GiftDiscountDetailDto? gifts { get; set; }
    }
}