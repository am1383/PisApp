namespace PisApp.API.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IAddressRepository Addresses { get; }
        
        Task<int> CompleteAsync();
    }
}