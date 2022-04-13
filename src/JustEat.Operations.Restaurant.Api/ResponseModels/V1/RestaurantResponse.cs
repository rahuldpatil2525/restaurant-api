using System.Collections.Generic;

namespace JustEat.Operations.Restaurant.Api.ResponseModels.V1
{
    public class RestaurantResponse
    {
        public int Count { get; set; }

        public IList<Restaurant> Restaurants { get; set; }
    }
}
