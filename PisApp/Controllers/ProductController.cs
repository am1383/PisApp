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
    [Vip]
    [Route("api/v1")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("motherboard/list")]
        public async Task<ResponseDto<IEnumerable<MotherboardDetailsDto>>> MotherboardList()
        {
            try
            {
                var motherboardList = await productService.GetAllMotherboard();

                return new ResponseDto<IEnumerable<MotherboardDetailsDto>>(true, motherboardList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<MotherboardDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("ram/list")]
        public async Task<ResponseDto<IEnumerable<RamDetailDto>>> RamList()
        {
            try
            {
                var ramList = await productService.GetAllRam();

                return new ResponseDto<IEnumerable<RamDetailDto>>(true, ramList);
                
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<RamDetailDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("cooler/list")]
        public async Task<ResponseDto<IEnumerable<CoolerDetailsDto>>> CoolerList()
        {
            try
            {
                var coolerList = await productService.GetAllCooler();

                return new ResponseDto<IEnumerable<CoolerDetailsDto>>(true, coolerList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CoolerDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("cpu/list")]
        public async Task<ResponseDto<IEnumerable<CpuDetailsDto>>> CupList()
        {
            try
            {
                var cpuList = await productService.GetAllCpu();

                return new ResponseDto<IEnumerable<CpuDetailsDto>>(true, cpuList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<CpuDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("gpu/list")]
        public async Task<ResponseDto<IEnumerable<GpuDetailsDto>>> GpuList()
        {
            try
            {
                var gpuList = await productService.GetAllGpu();

                return new ResponseDto<IEnumerable<GpuDetailsDto>>(true, gpuList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<GpuDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("supply/list")]
        public async Task<ResponseDto<IEnumerable<PowerSupplyDetailsDto>>> PowerSupplyList()
        {
            try
            {
                var powerSupplyList = await productService.GetAllPowerSupply();

                return new ResponseDto<IEnumerable<PowerSupplyDetailsDto>>(true, powerSupplyList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<PowerSupplyDetailsDto>>(false, default!, $"{e.Message}");
            }
        }

        [HttpGet("ssd/list")]
        public async Task<ResponseDto<IEnumerable<SsdDetailDto>>> SSDList()
        {
            try
            {
                var ssdList = await productService.GetAllSsd();

                return new ResponseDto<IEnumerable<SsdDetailDto>>(true, ssdList);
            }
            catch(Exception e)
            {
                return new ResponseDto<IEnumerable<SsdDetailDto>>(false, default!, $"{e.Message}");
            }
        }
    }
}