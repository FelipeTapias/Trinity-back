using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection Infrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
