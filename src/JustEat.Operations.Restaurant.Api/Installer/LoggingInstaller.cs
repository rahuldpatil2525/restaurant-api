using JustEat.Operations.Restaurant.Api.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace JustEat.Operations.Restaurant.Api.Installer
{
    public static class LoggingInstaller
    {
        public static IServiceCollection InstallLogging(this IServiceCollection services, IConfiguration configuration)
        {
            //Add application insights telimetry
            services.AddApplicationInsightsTelemetry(configuration);
            services.TryAddSingleton<IInstrumentor, ApplicationInsightsInstrumentor>();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration);
                builder.AddConsole();
            });

            return services;
        }
    }
}
