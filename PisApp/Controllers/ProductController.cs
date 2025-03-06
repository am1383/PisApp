using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Products.Dtos.Common;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Vip]
    [Route("api/v1")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("motherboard/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> MotherboardList()
        {
            try
            {
                var motherboardList = await productService.GetAllMotherboard();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(motherboardList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("ram/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> RamList()
        {
            try
            {
                var ramList = await productService.GetAllRam();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(ramList);
                
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("cooler/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> CoolerList()
        {
            try
            {
                var coolerList = await productService.GetAllCooler();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(coolerList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("cpu/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> CupList()
        {
            try
            {
                var cpuList = await productService.GetAllCpu();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(cpuList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("gpu/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> GpuList()
        {
            try
            {
                var gpuList = await productService.GetAllGpu();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(gpuList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("supply/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> PowerSupplyList()
        {
            try
            {
                var powerSupplyList = await productService.GetAllPowerSupply();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(powerSupplyList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }

        [HttpGet("ssd/list")]
        public async Task<ResponseDto<IEnumerable<CommonProductsDto>>> SSDList()
        {
            try
            {
                var ssdList = await productService.GetAllSsd();

                return new ResponseDto<IEnumerable<CommonProductsDto>>(ssdList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CommonProductsDto>>(default!, $"{e.Message}");
            }
        }
    }
}