using Aplication.Interfaces.Application;
using Aplication.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication
{
    public static class AplicationDependencyInjection
    {
        public static IServiceCollection Application(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
