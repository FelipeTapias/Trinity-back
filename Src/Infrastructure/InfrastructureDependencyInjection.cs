using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure.Service.Authenticacion.JwtToken;
using Infrastructure.Service.Mongo.Database.Context;
using Infrastructure.Service.Mongo.Database.Repository;
using Infrastructure.Service.RedisCache.Adapter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        
        public static IServiceCollection Infrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICustomerUserRepository<CustomerUser>, CustomerUserRepository<CustomerUser>>();
            services.AddScoped<IProductRepository<Product>, ProductRepository<Product>>();
            services.AddScoped<IPaymentRepository<Payment>, PaymentRepository<Payment>>();
            services.AddScoped<IAdminUserRepository<AdministratorUser>, AdminUserRepository<AdministratorUser>>();
            services.AddScoped<IUserCacheRepository, UserCacheAdapter>();

            return services;
        }

        #region Authentication

        public static IServiceCollection RegisterJwtToken(this IServiceCollection services, string secretKey, int expireIn)
        {
            services.AddSingleton<ITokenGenerator>(provider =>  
                new JwtTokenGenerator(secretKey, expireIn));

            return services;
        }

        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, string key)
        {
            services.AddAuthentication(config => {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config => {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            return services;
        }

        #endregion Authentication

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
