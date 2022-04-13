using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JustEat.Operations.Restaurant.Api.Constants;
using JustEat.Operations.Restaurant.Api.Contracts;
using Microsoft.Extensions.Logging;

namespace JustEat.Operations.Restaurant.Api.Clients
{
    public interface IRestaurantApiClient
    {
        Task<RestaurantApiResponse> GetRestaurantsAsync(string postCode, CancellationToken ct);
    }
    public class RestaurantApiClient : IRestaurantApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<RestaurantApiClient> _logger;

        public RestaurantApiClient(HttpClient httpClient, ILogger<RestaurantApiClient> logger)
        {
            _client = httpClient;
            _logger = logger;
        }

        public async Task<RestaurantApiResponse> GetRestaurantsAsync(string postCode, CancellationToken ct)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, postCode);

            var response = await _client.SendAsync(request, ct);

            return await ValidateAndGetResponseAsync(response, postCode, ct);
        }

        private async Task<RestaurantApiResponse> ValidateAndGetResponseAsync(HttpResponseMessage response, string postCode, CancellationToken ct)
        {
            if (response.Content == null)
            {
                return RestaurantApiResponse.Error(response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync(ct);

            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<RestaurantApiResponse>(responseContent);

            return LogErrorAndGetResponse(response, responseContent, postCode);
        }

        private RestaurantApiResponse LogErrorAndGetResponse(HttpResponseMessage response, string responseContent, string postCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var toManyRequestResponse = JsonSerializer.Deserialize<RestaurantApiTooManyRequestResponse>(responseContent);

                _logger.LogError(EventIds.RestaurantApiToManyRequests, $"Failed to get restaurant information for post code : {postCode}.", toManyRequestResponse.Fault.FaultId, toManyRequestResponse.Fault.TraceId, toManyRequestResponse.Fault.Errors);

                return RestaurantApiResponse.Error(response.StatusCode, toManyRequestResponse.Fault.Errors.FirstOrDefault()?.Description);
            }

            var error = JsonSerializer.Deserialize<RestaurantApiErrorResponse>(responseContent);

            _logger.LogError(EventIds.RestaurantApiError, $"Failed to get restaurant information for post code : {postCode}.", error.ExceptionMessage, error.Message, error.ExceptionType);

            return RestaurantApiResponse.Error(response.StatusCode, error.Message);
        }
    }
}
