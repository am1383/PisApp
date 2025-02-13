namespace PisApp.API.Interfaces
{
    public interface IReferRepository
    {
        public Task<int> CountUserReferrerByCode(string referCode);
    }
}