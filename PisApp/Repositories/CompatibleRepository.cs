using Microsoft.EntityFrameworkCore;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;
using PisApp.API.Products.Entities.Common;
using Npgsql;

namespace PisApp.API.Repositories
{
    public class CompatibleRepository(IUnitOfWork unitOfWork) : ICompatibleRepository
    {
        public async Task<List<Product>> GetCompatiblePartsAsync(int[] selectedPartIds, string partType)
        {
            var query = @"
                WITH SelectedParts AS (
                    SELECT unnest(@selectedPartIds) AS product_id
                ),
                CompatibleParts AS (
                    SELECT motherboard_id AS product_id, 'motherboard' AS type 
                    FROM compatible_mc_socket
                    WHERE cpu_id IN (SELECT product_id FROM SelectedParts)
                    
                    UNION ALL
                    
                    SELECT ram_id AS product_id, 'ram_stick' AS type 
                    FROM compatible_rm_slot
                    WHERE motherboard_id IN (SELECT product_id FROM SelectedParts)
                    
                    UNION ALL
                    
                    SELECT gpu_id AS product_id, 'gpu' AS type 
                    FROM compatible_gm_slot
                    WHERE motherboard_id IN (SELECT product_id FROM SelectedParts)
                    
                    UNION ALL
                    
                    SELECT ssd_id AS product_id, 'ssd' AS type 
                    FROM compatible_sm_slot
                    WHERE motherboard_id IN (SELECT product_id FROM SelectedParts)
                    
                    UNION ALL
                    
                    SELECT gpu_id AS product_id, 'gpu' AS type 
                    FROM compatible_gp_connector
                    WHERE power_supply_id IN (SELECT product_id FROM SelectedParts)
                    
                    UNION ALL
                    
                    SELECT cooler_id AS product_id, 'cooler' AS type 
                    FROM compatible_cc_socket
                    WHERE cpu_id IN (SELECT product_id FROM SelectedParts)
                )
                SELECT p.*, cp.type
                FROM product p
                JOIN CompatibleParts cp ON p.id = cp.product_id
                WHERE (@p0 IS NULL OR @p0 = '' OR cp.type = @p0);
            ";

            var selectedPartIdsParam = new NpgsqlParameter("@selectedPartIds", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = selectedPartIds
            };

            var partTypeParam = new NpgsqlParameter("@p0", NpgsqlTypes.NpgsqlDbType.Text)
            {
                Value = string.IsNullOrEmpty(partType) ? (object)DBNull.Value : partType
            };

            return await unitOfWork.Context.Set<Product>()
                                           .FromSqlRaw(query, selectedPartIdsParam, partTypeParam)
                                           .ToListAsync();
        }

    }
}