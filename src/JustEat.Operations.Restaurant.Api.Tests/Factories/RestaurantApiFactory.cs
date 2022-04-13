using System;
using System.Net;
using System.Net.Http;
using JustEat.Operations.Restaurant.Api.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace JustEat.Operations.Restaurant.Api.Tests.Factories
{
    public class RestaurantApiFactory : WebApplicationFactory<Startup>
    {
        private readonly Fakes.FakeHttpMessageHandler _messageHandler;

        public RestaurantApiFactory()
        {
            SetEnvironmentVariable();
            Instrumentor = new();
            HttpClientFactory = new();
            
            _messageHandler = new();

            SetupHttpClient();
        }

        public Mock<IInstrumentor> Instrumentor { get; }

        public Mock<IHttpClientFactory> HttpClientFactory { get; }

        public void SetResponse(HttpStatusCode status, string response)
        {
            _messageHandler.ResponseMessage = new HttpResponseMessage(status)
            {
                Content = new StringContent(response)
            };
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.Add(ServiceDescriptor.Singleton(typeof(IInstrumentor), Instrumentor.Object));
                services.Add(ServiceDescriptor.Singleton(typeof(IHttpClientFactory), HttpClientFactory.Object));
            });
        }
        
        private void SetEnvironmentVariable()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Functional");
        }

        private void SetupHttpClient()
        {
            var fakeHttpClient = new HttpClient(_messageHandler)
            {
                BaseAddress = new Uri("https://test-api-restaurant.com")
            };

            HttpClientFactory.Setup(x => x.CreateClient("RestaurantApi")).Returns(fakeHttpClient);
        }
    }
}
