using Microsoft.EntityFrameworkCore;
using PisApp.API.DbContextes;
using PisApp.API.Entities;
using PisApp.API.Interfaces;

namespace PisApp.API.Repositories
{
    public class CompatibleRepository : ICompatibleRepository
    {
        private readonly PisAppDbContext _context;

        public CompatibleRepository(PisAppDbContext context)
        {   
            _context = context;
        }

        public async Task<bool> CompatibleCCSocketChecker(int coolerId, int cpuId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_cc_socket WHERE cpu_id = @p0 AND cooler_id = @p1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, cpuId, coolerId) 
                                       .FirstOrDefaultAsync();

            return result.exists;
        }

        public async Task<bool> CompatibleGpConnectChecker(int gpuId, int powerSupplyId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_gp_connector WHERE gpu_id = @p0 AND power_supply_id = @p1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, gpuId, powerSupplyId)
                                       .FirstOrDefaultAsync();
            return result.exists;
        }

        public async Task<bool> CompatibleMcSocketChecker(int cpuId, int motherboardId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_mc_socket WHERE cpu_id = @p0 AND motherboard_id = @p1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, cpuId, motherboardId)
                                       .FirstOrDefaultAsync();
            return result.exists;
        }

        public async Task<bool> CompatibleRmSlotChecker(int ramId, int motherboardId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_rm_slot WHERE ram_id = @p0 AND motherboard_id = @P1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, ramId, motherboardId)
                                       .FirstOrDefaultAsync();
            return result.exists;
        }

        public async Task<bool> CompatibleGmSlotChecker(int gpuId, int motherboardId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_gm_slot WHERE gpu_id = @p0 AND motherboard_id = @p1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, gpuId, motherboardId)
                                       .FirstOrDefaultAsync();
            return result.exists;
        }

        public async Task<bool> CompatibleSmSlotChecker(int ssdId, int motherboardId)
        {
            var query = "SELECT EXISTS(SELECT * FROM compatible_sm_slot WHERE ssd_id = @p0 AND motherboard_id = @p1)";

            var result = await _context.Set<Compatible>()
                                       .FromSqlRaw(query, ssdId, motherboardId)
                                       .FirstOrDefaultAsync();
            return result.exists;
        }
    }
}