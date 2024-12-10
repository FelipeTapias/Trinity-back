using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure.Service.Mongo.Database.Context;
using Infrastructure.Service.Mongo.Database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        #region Mongo
        public static IServiceCollection Infrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository<User>, UserRepository<User>>();

            return services;
        }

        public static IServiceCollection RegisterMongo(this IServiceCollection services, 
                                                       string collectionName, 
                                                       string databaseName, 
                                                       string connectionString)
        {
            services.AddSingleton(cfg => new GenericContext(collectionName, connectionString, databaseName));

            return services;
        }

        #endregion Mongo
    }
}
