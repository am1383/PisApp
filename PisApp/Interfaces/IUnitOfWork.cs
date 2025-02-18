using PisApp.API.DbContextes;

namespace PisApp.API.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository         Users       { get; }
        PisAppDbContext         Context     { get; }
        IAddressRepository      Addresses   { get; }
        IProductRepository      Products    { get; }
        ICompatibleRepository   Compatibles { get; }
        IDiscountRepository     Discounts   { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        ITransactionRepository  Transactions  { get; }
        IReferRepository        Refers        { get; }
        Task<int>               CompleteAsync();
    }
}