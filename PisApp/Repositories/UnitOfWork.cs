using PisApp.API.DbContextes;
using PisApp.API.Interfaces;
using PisApp.API.Interfaces.UnitOfWork;

namespace PisApp.API.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public PisAppDbContext Context { get; }     

        public UnitOfWork(PisAppDbContext context)
        {
            Context = context;
        }   
        
        private IUserRepository?         _usersRepository;
        private ICompatibleRepository?   _compatibleRepository;
        private IAddressRepository?      _addressRepository;
        private ITransactionRepository?  _transactionRespository;
        private IProductRepository?      _productRepository;
        private IReferRepository?        _referRepository;
        private IShoppingCartRepository? _shoppingCartRepository;
        private IDiscountRepository?     _discountRepository;

        public IUserRepository Users
                => _usersRepository        ??= new UserRepository(this);
        public IDiscountRepository Discounts 
                => _discountRepository     ??= new DiscountRepository(this);
        public IProductRepository Products 
                => _productRepository      ??= new ProductRepository(this);
        public IShoppingCartRepository ShoppingCarts 
                => _shoppingCartRepository ??= new ShoppingCartRepository(this);
        public IReferRepository Refers              
                => _referRepository        ??= new ReferRepository(this);
        public IAddressRepository Addresses 
                => _addressRepository      ??= new AddressRepository(this);
        public ITransactionRepository Transactions
                => _transactionRespository ??= new TransactionRepository(this);
        public ICompatibleRepository Compatibles 
                => _compatibleRepository   ??= new CompatibleRepository(this);

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}