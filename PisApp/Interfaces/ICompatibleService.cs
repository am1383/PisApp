using PisApp.API.Compatibles.Dtos;

namespace PisApp.API.Interfaces
{
    public interface ICompatibleService
    {
        public Task<CompatibleResultDto> GetCompatibleCCSocket(int coolerId, int cpuId);
        public CompatibleResultDto CompatibleResult(bool compatibleResult);
        public Task<CompatibleResultDto> GetCompatibleGpConnect(int gpuId, int powerSupplyId);
        public Task<CompatibleResultDto> GetCompatibleMcSocket(int cpuId, int motherboardId);
        public Task <CompatibleResultDto> GetCompatibleRmSlot(int ramId, int motherboardId);
        public Task <CompatibleResultDto> GetCompatibleGmSlot(int gpuId, int motherboardId); 
        public Task<CompatibleResultDto> GetCompatibleSmSlot(int ssdId, int motherboardId);
    }
}