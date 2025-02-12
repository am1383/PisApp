using PisApp.API.DbContextes;
using PisApp.API.Interface;
using PisApp.API.Interface.UnitOfWork;

namespace PisApp.API.Repositories.UnitOfWork
{
    public class UnitOfWork(PisAppDb context) : IUnitOfWork
    {
        private readonly PisAppDb _context = context;
        
        private IUserRepository? _usersRepository;

        public IUserRepository Users => _usersRepository ??= new UserRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}