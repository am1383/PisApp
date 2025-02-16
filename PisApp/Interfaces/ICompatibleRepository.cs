namespace PisApp.API.Interfaces
{
    public interface ICompatibleRepository
    {
        public Task<bool> CompatibleGpConnectChecker(int gpuId, int powerSupplyId);
        public Task<bool> CompatibleMcSocketChecker(int cpuId, int motherboardId);
        public Task<bool> CompatibleRmSlotChecker(int ramId, int motherboardId);
        public Task<bool> CompatibleGmSlotChecker(int gpuId, int motherboardId);
        public Task<bool> CompatibleSmSlotChecker(int ssdId, int motherboardId);
        public Task<bool> CompatibleCCSocketChecker(int coolerId, int cpuId);
    }
}