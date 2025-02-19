using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class ReferRepository(IUnitOfWork unitOfWork) : IReferRepository
    {
        public async Task<int> CountUserReferrerByCode(string referCode)
        {
            var query  = "SELECT COUNT(*) FROM refers WHERE referrer_id = @p0";
                
            var result = await unitOfWork.Context.Set<Refer>()
                                                 .FromSqlRaw(query, referCode)
                                                 .FirstOrDefaultAsync();

            return result.count;
        }
    }
}