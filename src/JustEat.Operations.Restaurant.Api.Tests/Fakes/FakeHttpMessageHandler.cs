using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustEat.Operations.Restaurant.Api.Tests.Fakes
{
    class FakeHttpMessageHandler : HttpMessageHandler
    {
        public HttpResponseMessage ResponseMessage { get; set; }

        public Action<HttpRequestMessage> RequestValidationFunction { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestValidationFunction?.Invoke(request);
            return await Task.FromResult(ResponseMessage);
        }
    }
}
