using System;
using System.Net.Http;
using JustEat.Operations.Restaurant.Api.Builders;
using JustEat.Operations.Restaurant.Api.Clients;
using JustEat.Operations.Restaurant.Api.Configurations;
using JustEat.Operations.Restaurant.Api.Mapper;
using JustEat.Operations.Restaurant.Api.Providers;
using JustEat.Operations.Restaurant.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace JustEat.Operations.Restaurant.Api.Installer
{
    public static class ServiceInstaller
    {
        public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var restaurantConfig = configuration.GetSection("RestaurantApiConfiguration").Get<RestaurantApiConfiguration>();

            services.AddSingleton<IRestaurantGateway, RestaurantGateway>();
            services.AddSingleton<IRestaurantCoreMapper, RestaurantCoreMapper>();
            services.AddSingleton<IRetryPolicyProvider, RetryPolicyProvider>();
            services.AddSingleton<IRestaurantResponseBuilder, RestaurantResponseBuilder>();

            services.AddHttpClient<IRestaurantApiClient, RestaurantApiClient>("RestaurantApi",
                (client) =>
                {
                    client.BaseAddress = new Uri(restaurantConfig.BaseUrl);
                    client.DefaultRequestHeaders.Add("justeat.application", "JustEat.Operations.Restaurant.API");
                })
                .AddPolicyHandler(GetRetryPolicy(configuration));

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration configuration)
        {
            var policyConfig = configuration.GetSection("RetryPolicyConfiguration").Get<RetryPolicyConfiguration>();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(policyConfig.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(policyConfig.RetryInterval, retryAttempt)));
        }
    }
}
