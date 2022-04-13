using System.Collections.Generic;
using System.Net;
using JustEat.Operations.Restaurant.Api.Constants;

namespace JustEat.Operations.Restaurant.Api.CoreModels
{
    public class RestaurantResult
    {
        public RestaurantResult(IEnumerable<RestaurantCore> restaurants, int resultCount)
        {
            Restaurants = restaurants;
            ResultCount = resultCount;
        }

        public RestaurantResult(HttpStatusCode statusCode, string message)
        {
            ErrorCode = GetErrorCode(statusCode);
            ErrorMessage = message;
            HasError = true;
        }



        public int ResultCount { get; }

        public IEnumerable<RestaurantCore> Restaurants { get; }

        public string ErrorCode { get; }

        public bool HasError { get; }

        public string ErrorMessage { get; }

        private static string GetErrorCode(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => ErrorCodes.BadRequest,
                HttpStatusCode.InternalServerError => ErrorCodes.InternalServerError,
                HttpStatusCode.TooManyRequests => ErrorCodes.InternalServerError,
                HttpStatusCode.Unauthorized => ErrorCodes.DependencyFailed,
                _ => string.Empty,
            };
        }
    }
}
