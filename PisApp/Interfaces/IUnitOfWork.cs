namespace PisApp.API.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository         Users { get; }
        IProductRepository      Products { get; }
        IAddressRepository      Addresses { get; }
        ICompatibleRepository   Compatibles { get; }
        IDiscountRepository     Discounts { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        ITransactionRepository  Transactions { get; }
        IReferRepository        Refers { get; }
        Task<int>               CompleteAsync();
    }
}