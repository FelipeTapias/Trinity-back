namespace Aplication.Interfaces.Infrastructure
{
    public interface IUserCacheRepository
    {
        Task SaveUserlist(string usersList);
        Task<string> GetUserlist(string keyUserList);
    }
}
