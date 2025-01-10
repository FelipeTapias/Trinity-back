namespace RestService.Trinity.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection RegisterCors(this IServiceCollection services)
        {
            services.AddCors(opts => {
                opts.AddPolicy("ClientPolicy", app => {
                    app.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            return services;
        }
    }
}
