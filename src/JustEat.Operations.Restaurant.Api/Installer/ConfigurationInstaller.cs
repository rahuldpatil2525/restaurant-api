using JustEat.Operations.Restaurant.Api.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JustEat.Operations.Restaurant.Api.Installer
{
    public static class ConfigurationInstaller
    {
        public static IServiceCollection InstallConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IRetryPolicyConfiguration>(x => configuration.GetSection("RetryPolicyConfiguration").Get<RetryPolicyConfiguration>());

            return services;
        }
    }
}
