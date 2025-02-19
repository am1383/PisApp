using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PisApp.API.Compatibles.Dtos;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Vip]
    [Route("api/v1")]
    public class CompatibleController(ICompatibleService compatibleService) : ControllerBase
    {
        [HttpPost("compatible/ccSocket")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleCCSocket(CompatibleCCSocketRequestDto dto)
        {
            try
            {
                var coolerId         = dto.cooler_id;
                
                var cpuId            = dto.cpu_id;

                var compatibleResult = await compatibleService.GetCompatibleCCSocket(coolerId, cpuId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }

        [HttpPost("compatible/rmSlot")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleRmSlot(ComapatibleRmSlotRequestDto dto)
        {
            try
            {
                var ramId            = dto.ram_id;

                var motherboardId    = dto.motherboard_id;
                
                var compatibleResult = await compatibleService.GetCompatibleRmSlot(ramId, motherboardId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }

        [HttpPost("compatible/gmSlot")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleGmSlot(CompatibleGmSlotRequestDto dto)
        {
            try
            {
                var gpuId            = dto.gpu_id;

                var motherboardId    = dto.motherboard_id;

                var compatibleResult = await compatibleService.GetCompatibleGmSlot(gpuId, motherboardId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }

        [HttpPost("compatible/mcSocket")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleMcSocket(CompatibleMcSocketRequestDto dto)
        {
            try
            {
                var cpuId            = dto.cpu_id;

                var motherboardId    = dto.motherboard_id;

                var compatibleResult = await compatibleService.GetCompatibleMcSocket(cpuId, motherboardId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }

        [HttpPost("compatible/smSlot")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleSmSlot(CompatibleSmSlotRequestDto dto)
        {
            try
            {    
                var ssdId            = dto.ssd_id;

                var motherboardId    = dto.motherboard_id;

                var compatibleResult = await compatibleService.GetCompatibleSmSlot(ssdId, motherboardId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }

        [HttpPost("compatible/gpConnect")]
        public async Task<ResponseDto<CompatibleResultDto>> CompatibleGpConnect(CompatibleGpConnectRequestDto dto)
        {
            try
            {
                var gpuId            = dto.gpu_id;

                var powerSupplyId    = dto.power_supply_id;

                var compatibleResult = await compatibleService.GetCompatibleGpConnect(gpuId, powerSupplyId);

                return new ResponseDto<CompatibleResultDto>(true, compatibleResult);
            }
            catch(Exception e)
            {
                return new ResponseDto<CompatibleResultDto>(false, default!, $"{e.Message}");
            }
        }
    }
}