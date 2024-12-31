using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure.Service.Mongo.Database.Context;
using Infrastructure.Service.Mongo.Database.Repository;
using Infrastructure.Service.RedisCache.Adapter;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        
        public static IServiceCollection Infrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository<User>, UserRepository<User>>();
            services.AddScoped<IUserCacheRepository, UserCacheAdapter>();

            return services;
        }

        #region Mongo
        public static IServiceCollection RegisterMongo(this IServiceCollection services,
                                                       string databaseName, 
                                                       string connectionString)
        {
            services.AddSingleton(cfg => new GenericContext(connectionString, databaseName));

            return services;
        }
        #endregion Mongo

        #region RedisCache
        public static IServiceCollection RegisterRedisCache(this IServiceCollection services,
                                                            string connectionString,
                                                            int dbNumber)
        {
            services.AddSingleton(cfg => LazyConnection(connectionString).Value.GetDatabase(dbNumber));

            return services;
        }
        #endregion RedisCache

        private static Lazy<ConnectionMultiplexer> LazyConnection(string connectionsString) =>
            new(() =>
            {
                return ConnectionMultiplexer.Connect(connectionsString);
            });
    }
}
