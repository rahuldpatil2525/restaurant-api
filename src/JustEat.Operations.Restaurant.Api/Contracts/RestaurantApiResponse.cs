using System.Collections.Generic;
using System.Net;

namespace JustEat.Operations.Restaurant.Api.Contracts
{
    public class MetaData
    {
        public int ResultCount { get; set; }
    }

    public class RestaurantApiResponse
    {
        public MetaData MetaData { get; set; }

        public IList<Restaurant> Restaurants { get; set; }

        public RestaurantApiStatus Status { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public static RestaurantApiResponse Error(HttpStatusCode statusCode, string message = "")
        {
            return new RestaurantApiResponse()
            {
                Status = RestaurantApiStatus.Error,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
