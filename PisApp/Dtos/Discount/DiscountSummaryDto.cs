namespace PisApp.API.Dtos
{
    public class DiscountSummaryDto
    {
        public IEnumerable<PrivateDiscountDetailsDto>? discounts { get; set; }

        public GiftDiscountDetailDto? gifts                      { get; set; }
    }
}