using System;
using System.Net.Http;
using JustEat.Operations.Restaurant.Api.Configurations;
using JustEat.Operations.Restaurant.Api.Constants;
using JustEat.Operations.Restaurant.Api.Logging;
using Polly;

namespace JustEat.Operations.Restaurant.Api.Providers
{
    public interface IRetryPolicyProvider
    {
        IAsyncPolicy AsyncPolicy { get; }
    }

    public class RetryPolicyProvider : IRetryPolicyProvider
    {
        private readonly IInstrumentor _instrumentor;

        public RetryPolicyProvider(IRetryPolicyConfiguration retryPolicyConfiguration, IInstrumentor instrumentor)
        {
            _instrumentor = instrumentor;

            AsyncPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(retryPolicyConfiguration.RetryAttempts, retryAttempt => TimeSpan.FromMilliseconds(retryPolicyConfiguration.RetryInterval * retryAttempt), OnRetry);
        }

        private void OnRetry(Exception exception, TimeSpan timeSpan)
        {
            _instrumentor.LogException(EventIds.RetryException, exception);
        }

        public IAsyncPolicy AsyncPolicy { get; }
    }
}
