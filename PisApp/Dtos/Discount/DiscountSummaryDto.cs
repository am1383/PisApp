namespace PisApp.API.Dtos
{
    public class DiscountSummaryDto
    {
        public IEnumerable<PrivateDiscountDetailDto>? discounts { get; set; }

        public GiftedDiscountDetailDto? gifts { get; set; }
    }
}