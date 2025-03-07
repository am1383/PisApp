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
    }
}