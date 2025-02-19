using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories
{
    public class ReferRepository : IReferRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReferRepository(IUnitOfWork unitOfWork)
        {   
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountUserReferrerByCode(string referCode)
        {
            var query = "SELECT COUNT(*) FROM refers WHERE referrer_id = @p0";
                
            var result = await _unitOfWork.Context.Set<Refer>()
                                                  .FromSqlRaw(query, referCode)
                                                  .FirstOrDefaultAsync();

            return result.count;
        }
    }
}