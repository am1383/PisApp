using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Products.Entities;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Vip]
    [Route("api/v1")]
    public class CompatibleController(ICompatibleService compatibleService) : ControllerBase
    {
        [HttpPost("compatible")]
        public async Task<ResponseDto<IEnumerable<ProductDetailsDto>>> GetCompatibleParts(CompatibleRequestDto dto, [FromQuery] string? type)
        {
            try
            {
                var compatiblesPart = await compatibleService.GetCompatibleParts(dto.products_id, type);

                return new ResponseDto<IEnumerable<ProductDetailsDto>>(compatiblesPart);
            }
            catch (Exception e)
            {
                return new ResponseDto<IEnumerable<ProductDetailsDto>>(default!, $"{e.Message}");
            }
        }
    }
}