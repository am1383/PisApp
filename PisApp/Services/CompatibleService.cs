using PisApp.API.Compatibles.Dtos;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Services
{
    public class CompatibleService : ICompatibleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompatibleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CompatibleResultDto CompatibleResult(bool compatibleResult)
        {
            return new CompatibleResultDto
            {
                result = compatibleResult
            };
        }

        public async Task<CompatibleResultDto> GetCompatibleCCSocket(int coolerId, int cpuId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleCCSocketChecker(coolerId, cpuId);

            return CompatibleResult(result);
        }

        public async Task<CompatibleResultDto> GetCompatibleGpConnect(int gpuId, int powerSupplyId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleGpConnectChecker(gpuId, powerSupplyId);

            return CompatibleResult(result);
        }

        public async Task<CompatibleResultDto> GetCompatibleMcSocket(int cpuId, int motherboardId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleMcSocketChecker(cpuId, motherboardId);

            return CompatibleResult(result);
        }

        public async Task <CompatibleResultDto> GetCompatibleRmSlot(int ramId, int motherboardId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleRmSlotChecker(ramId, motherboardId);
            
            return CompatibleResult(result);
        }

        public async Task <CompatibleResultDto> GetCompatibleGmSlot(int gpuId, int motherboardId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleGmSlotChecker(gpuId, motherboardId);

            return CompatibleResult(result);
        } 

        public async Task<CompatibleResultDto> GetCompatibleSmSlot(int ssdId, int motherboardId)
        {
            var result = await _unitOfWork.Compatibles
                                          .CompatibleSmSlotChecker(ssdId, motherboardId);
            
            return CompatibleResult(result);
        }
    }
}