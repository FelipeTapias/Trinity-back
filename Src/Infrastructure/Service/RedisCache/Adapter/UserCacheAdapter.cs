using Aplication.Interfaces.Infrastructure;
using StackExchange.Redis;

namespace Infrastructure.Service.RedisCache.Adapter
{
    public class UserCacheAdapter(IDatabase database) : IUserCacheRepository
    {
        private readonly IDatabase _database = database;

        public async Task SaveUserlist(string usersList)
        {
            await _database.StringSetAsync("Trinity_users", usersList);
            Task.CompletedTask.Wait();
        }

        public async Task<string> GetUserlist(string keyUserList)
        {
            return await _database.StringGetAsync(keyUserList);
        }
    }
}
