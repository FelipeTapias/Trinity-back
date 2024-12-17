using Serilog;

namespace RestService.Trinity.Configuration
{
    public static class HostConfiguration
    {
        public static IHostBuilder configureSerilog(this IHostBuilder hostBuilder,
                                    string logLevel) => 
            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console()
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
                .Enrich.FromLogContext();
            });
        

    }
}
