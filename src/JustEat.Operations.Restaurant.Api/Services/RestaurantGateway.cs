using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JustEat.Operations.Restaurant.Api.Clients;
using JustEat.Operations.Restaurant.Api.CoreModels;
using JustEat.Operations.Restaurant.Api.Mapper;

namespace JustEat.Operations.Restaurant.Api.Services
{
    public interface IRestaurantGateway
    {
        Task<RestaurantResult> GetRestaurantsAsync(string postCode, CancellationToken ct);
    }

    public class RestaurantGateway : IRestaurantGateway
    {
        private readonly IRestaurantCoreMapper _restaurantCoreMapper;
        private readonly IRestaurantApiClient _restaurantApiClient;

        public RestaurantGateway(IRestaurantCoreMapper restaurantCoreMapper, IRestaurantApiClient restaurantApiClient)
        {
            _restaurantCoreMapper = restaurantCoreMapper;
            _restaurantApiClient = restaurantApiClient;
        }

        public async Task<RestaurantResult> GetRestaurantsAsync(string postCode, CancellationToken ct)
        {
            var restaurantResponse = await _restaurantApiClient.GetRestaurantsAsync(postCode, ct).ConfigureAwait(false);

            if (restaurantResponse.Status == Contracts.RestaurantApiStatus.Found)
            {
                var restaurants = restaurantResponse.Restaurants.Where(x=>x.IsOpenNow).Select(x => _restaurantCoreMapper.Map(x));

                return new RestaurantResult(restaurants, restaurantResponse.MetaData.ResultCount);
            }

            return new RestaurantResult(restaurantResponse.StatusCode, restaurantResponse.Message);
        }
    }
}
