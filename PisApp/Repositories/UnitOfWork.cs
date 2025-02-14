using PisApp.API.DbContextes;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories.UnitOfWork
{
    public class UnitOfWork(PisAppDbContext context) : IUnitOfWork
    {
        private readonly PisAppDbContext _context = context;
        
        private IUserRepository?         _usersRepository;
        private IAddressRepository?      _addressRepository;
        private ITransactionRepository?  _transactionRespository;
        private IProductRepository?      _productRepository;
        private IReferRepository?        _referRepository;
        private IShoppingCartRepository? _shoppingCartRepository;
        private IDiscountRepository?     _discountRepository;

        public IUserRepository Users
                => _usersRepository ??= new UserRepository(_context);
        public IDiscountRepository Discounts 
                => _discountRepository ??= new DiscountRepository(_context);
        public IProductRepository Products 
                => _productRepository ??= new ProductRepository(_context);
        public IShoppingCartRepository ShoppingCarts 
                => _shoppingCartRepository ??= new ShoppingCartRepository(_context);
        public IReferRepository Refers              
                => _referRepository ??= new ReferRepository(_context);
        public IAddressRepository Addresses 
                => _addressRepository ??= new AddressRepository(_context);
        public ITransactionRepository Transactions
                => _transactionRespository ??= new TransactionRepository(_context);

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