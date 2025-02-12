using PisApp.API.DbContextes;
using PisApp.API.Interface;
using PisApp.API.Interface.UnitOfWork;

namespace PisApp.API.Repositories.UnitOfWork
{
    public class UnitOfWork(PisAppDbContext context) : IUnitOfWork
    {
        private readonly PisAppDbContext _context = context;
        
        private IUserRepository? _usersRepository;

        private IAddressRepository? _addressRepository;

        public IUserRepository Users => _usersRepository ??= new UserRepository(_context);

        public IAddressRepository Addresses => _addressRepository ??= new AddressRepository(_context);

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