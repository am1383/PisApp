using Microsoft.EntityFrameworkCore;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities.Common;
using Npgsql;

namespace PisApp.API.Repositories
{
    public class CompatibleRepository(IUnitOfWork unitOfWork) : ICompatibleRepository
    {
        public async Task<List<Product>> GetCompatiblePartsAsync(IEnumerable<int> selectedPartIds)
        {
            var query = @"SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM (
                    SELECT cooler_id AS compatible_id FROM compatible_cc_socket WHERE cpu_id = ANY(@selectedPartIds)
                    UNION
                    SELECT cpu_id FROM compatible_cc_socket WHERE cooler_id = ANY(@selectedPartIds)
                    
                    UNION ALL
                    
                    SELECT motherboard_id FROM compatible_mc_socket WHERE cpu_id = ANY(@selectedPartIds)
                    UNION
                    SELECT cpu_id FROM compatible_mc_socket WHERE motherboard_id = ANY(@selectedPartIds)
                    
                    UNION ALL
                    
                    SELECT motherboard_id FROM compatible_rm_slot WHERE ram_id = ANY(@selectedPartIds)
                    UNION
                    SELECT ram_id FROM compatible_rm_slot WHERE motherboard_id = ANY(@selectedPartIds)
                    
                    UNION ALL
                    
                    SELECT power_supply_id FROM compatible_gp_connector WHERE gpu_id = ANY(@selectedPartIds)
                    UNION
                    SELECT gpu_id FROM compatible_gp_connector WHERE power_supply_id = ANY(@selectedPartIds)
                    
                    UNION ALL
                    
                    SELECT motherboard_id FROM compatible_sm_slot WHERE ssd_id = ANY(@selectedPartIds)
                    UNION
                    SELECT ssd_id FROM compatible_sm_slot WHERE motherboard_id = ANY(@selectedPartIds)
                    
                    UNION ALL
                    
                    SELECT motherboard_id FROM compatible_gm_slot WHERE gpu_id = ANY(@selectedPartIds)
                    UNION
                    SELECT gpu_id FROM compatible_gm_slot WHERE motherboard_id = ANY(@selectedPartIds)
                ) AS compat
                JOIN product p ON p.id = compat.compatible_id
            ";

            var selectedPartIdsParam = new NpgsqlParameter("@selectedPartIds", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = selectedPartIds?.Any() == true ? selectedPartIds.ToArray() : new int[0]
            };

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, selectedPartIdsParam)
                                           .ToListAsync();
        }

        public async Task<List<Product>>ComptaibleWithHDD(int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage  >= @p0

                UNION

                SELECT id AS product_id, brand, model, category
                FROM product
                WHERE category IN ('CPU', 'SSD', 'HDD', 'RAM Stick')";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatibleWithCPU(int cpuId, int maxMemoryLimit, decimal baseFrequency, decimal BoostFrequncy, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_mc_socket cmc ON p.id = cmc.motherboard_id
                WHERE cmc.cpu_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN ram_stick rs ON p.id = rs.product_id
                WHERE rs.capacity >= @p1 AND rs.frequency BETWEEN @p2 AND @p3

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_cc_socket cc ON p.id = cc.cooler_id
                WHERE cc.cpu_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage >= @p4

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                WHERE p.category IN ('GPU', 'SSD', 'HDD')";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, cpuId, maxMemoryLimit, baseFrequency, BoostFrequncy, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatibleWithMotherboard(int motherboardId, decimal memorySpeedRange, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_mc_socket cmc ON p.id = cmc.cpu_id
                WHERE cmc.motherboard_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_rm_slot crm ON p.id = crm.ram_id
                JOIN ram_stick rs ON p.id = rs.product_id
                WHERE rs.frequency       <= @p1

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_gm_slot cgm ON p.id = cgm.gpu_id
                WHERE cgm.motherboard_id            = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_sm_slot csm ON p.id = csm.ssd_id
                WHERE csm.motherboard_id            = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage  >= @p2

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                WHERE p.category IN ('Cooler', 'HDD')
            ";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, motherboardId, memorySpeedRange, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatibleWithRAM(int ramId, decimal frequency, decimal capacity, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN motherboard m ON p.id = m.product_id
                JOIN compatible_rm_slot crm ON m.product_id = crm.motherboard_id
                WHERE crm.ram_id = @p0
                AND m.memory_speed_range >= @p1

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN cpu c ON p.id        = c.product_id
                WHERE c.max_memory_limit <= @p2
                AND @p3 BETWEEN c.base_frequency AND c.boost_frequency

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage >= @p4

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                WHERE p.category IN ('GPU', 'Cooler', 'SSD', 'HDD')";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, ramId, frequency, capacity, frequency, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatbileWithCooler(int coolerId, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_cc_socket ccs ON p.id = ccs.cpu_id
                WHERE ccs.cooler_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage >= @p1

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                WHERE p.category IN ('Motherboard', 'HDD', 'SSD', 'GPU', 'RAM Stick')
            ";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, coolerId, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatibleWithSSD(int ssdId, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_sm_slot csm ON p.id = csm.motherboard_id
                WHERE csm.ssd_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage >= @p1

                UNION

                SELECT id AS product_id, brand, model, category
                FROM product
                WHERE category IN ('Motherboard', 'HDD', 'CPU', 'GPU', 'RAM Stick')";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, ssdId, supportedWattage)
                                           .ToListAsync();
        }

        public async Task<List<Product>> CompatibleWithGPU(int gpuId, int supportedWattage)
        {
            var query = @"
                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_gm_slot cgm ON p.id = cgm.motherboard_id
                WHERE cgm.gpu_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN compatible_gp_connector cgp ON p.id = cgp.power_supply_id
                WHERE cgp.gpu_id = @p0

                UNION

                SELECT p.id AS product_id, p.brand, p.model, p.category
                FROM product p
                JOIN power_supply ps ON p.id = ps.product_id
                WHERE ps.supported_wattage >= @p1

                UNION

                SELECT id AS product_id, brand, model, category
                FROM product
                WHERE category IN ('CPU', 'SSD', 'HDD', 'RAM Stick')";

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, gpuId, supportedWattage)
                                           .ToListAsync();
        }
    }
}