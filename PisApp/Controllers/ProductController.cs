using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PisApp.API.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Products.Dtos;
using PisApp.API.Products.Entities;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("motherboard/list")]
        public async Task<ResponseDto<IEnumerable<MotherboardDetailsDto>>> MotherboardList()
        {
            try
            {
                var motherboard = await _productService.GetAllMotherboard();

                return new ResponseDto<IEnumerable<MotherboardDetailsDto>>(true, motherboard);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<MotherboardDetailsDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("ram/list")]
        public async Task<ResponseDto<IEnumerable<RamDetailDto>>> RamList()
        {
            try
            {
                var ram = await _productService.GetAllRam();

                return new ResponseDto<IEnumerable<RamDetailDto>>(true, ram);
                
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<RamDetailDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("cooler/list")]
        public async Task<ResponseDto<IEnumerable<CoolerDetailsDto>>> CoolerList()
        {
            try
            {
                var cooler = await _productService.GetAllCooler();

                return new ResponseDto<IEnumerable<CoolerDetailsDto>>(true, cooler);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CoolerDetailsDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("cpu/list")]
        public async Task<ResponseDto<IEnumerable<CpuDetailsDto>>> CupList()
        {
            try
            {
                var cpu = await _productService.GetAllCpu();

                return new ResponseDto<IEnumerable<CpuDetailsDto>>(true, cpu);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CpuDetailsDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("gpu/list")]
        public async Task<ResponseDto<IEnumerable<GpuDetailsDto>>> GpuList()
        {
            try
            {
                var gpu = await _productService.GetAllGpu();

                return new ResponseDto<IEnumerable<GpuDetailsDto>>(true, gpu);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<GpuDetailsDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("supply/list")]
        public async Task<ResponseDto<IEnumerable<PowerSupplyDetailsDto>>> PowerSupplyList()
        {
            try
            {
                var powerSupply = await _productService.GetAllPowerSupply();

                return new ResponseDto<IEnumerable<PowerSupplyDetailsDto>>(true, powerSupply);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<PowerSupplyDetailsDto>>(false, null, $"Exception : {e.Message}");
            }
        }

        [HttpGet("ssd/list")]
        public async Task<ResponseDto<IEnumerable<SsdDetailDto>>> SSDList()
        {
            try
            {
                var ssd = await _productService.GetAllSsd();

                return new ResponseDto<IEnumerable<SsdDetailDto>>(true, ssd);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<SsdDetailDto>>(false, null, $"Exception : {e.Message}");
            }
        }
    }
}