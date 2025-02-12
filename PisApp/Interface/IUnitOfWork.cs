namespace PisApp.API.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}