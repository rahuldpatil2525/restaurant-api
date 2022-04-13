using System.Collections.Generic;
using System.Linq;
using JustEat.Operations.Restaurant.Api.CoreModels;
using JustEat.Operations.Restaurant.Api.ResponseModels.V1;

namespace JustEat.Operations.Restaurant.Api.Builders
{
    public interface IRestaurantResponseBuilder
    {
        public RestaurantResponse Build(RestaurantResult restaurantResult);
    }

    public class RestaurantResponseBuilder : IRestaurantResponseBuilder
    {
        public RestaurantResponse Build(RestaurantResult restaurantResult)
        {
            return new RestaurantResponse()
            {
                Count = restaurantResult.ResultCount,
                Restaurants = MapRestaurants(restaurantResult.Restaurants)
            };
        }

        private static IList<ResponseModels.V1.Restaurant> MapRestaurants(IEnumerable<RestaurantCore> restaurants)
        {
            if (restaurants == null || !restaurants.Any())
            {
                return Enumerable.Empty<ResponseModels.V1.Restaurant>().ToList();
            }

            return restaurants.Select(x => MapRestaurant(x)).ToList();
        }

        private static ResponseModels.V1.Restaurant MapRestaurant(RestaurantCore restaurantCore)
        {
            return new ResponseModels.V1.Restaurant()
            {
                City = restaurantCore.City,
                Name = restaurantCore.Name,
                Postcode = restaurantCore.Postcode,
                IsOpenNow=restaurantCore.IsOpenNow,
                Rating = MapRating(restaurantCore.Rating),
                CuisineTypes = MapCuisineTypes(restaurantCore.CuisineTypes)
            };
        }

        private static IEnumerable<CuisineType> MapCuisineTypes(IEnumerable<CuisineTypeCore> cuisineTypes)
        {
            if (cuisineTypes == null || !cuisineTypes.Any())
            {
                return Enumerable.Empty<CuisineType>();
            }

            return cuisineTypes.Select(x => MapCuisineType(x));
        }

        private static CuisineType MapCuisineType(CuisineTypeCore cuisineTypeCore)
        {
            return new CuisineType()
            {
                Name = cuisineTypeCore.Name,
                SeoName = cuisineTypeCore.SeoName
            };
        }

        private static Rating MapRating(RatingCore ratingCore)
        {
            if (ratingCore == null)
            {
                return null;
            }

            return new Rating()
            {
                Average = ratingCore.Average,
                Count = ratingCore.Count,
                StarRating = ratingCore.StarRating
            };
        }
    }
}
