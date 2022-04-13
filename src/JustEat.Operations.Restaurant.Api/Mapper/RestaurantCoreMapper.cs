using System.Collections.Generic;
using System.Linq;
using JustEat.Operations.Restaurant.Api.Contracts;
using JustEat.Operations.Restaurant.Api.CoreModels;

namespace JustEat.Operations.Restaurant.Api.Mapper
{
    public interface IRestaurantCoreMapper
    {
        RestaurantCore Map(Contracts.Restaurant restaurant);
    }

    public class RestaurantCoreMapper : IRestaurantCoreMapper
    {
        public RestaurantCore Map(Contracts.Restaurant restaurant)
        {
            return new RestaurantCore()
            {
                Name = restaurant.Name,
                City = restaurant.City,
                Postcode = restaurant.Postcode,
                IsOpenNow=restaurant.IsOpenNow,
                Rating = MapRating(restaurant.Rating),
                CuisineTypes = MapCuisineTypes(restaurant.CuisineTypes)
            };
        }

        private IEnumerable<CuisineTypeCore> MapCuisineTypes(IList<CuisineType> cuisineTypes)
        {
            if (cuisineTypes == null || !cuisineTypes.Any())
            {
                return Enumerable.Empty<CuisineTypeCore>();
            }

            return cuisineTypes.Select(x => MapCuisineType(x));
        }

        private CuisineTypeCore MapCuisineType(CuisineType cuisineType)
        {
            return new CuisineTypeCore()
            {
                Name = cuisineType.Name,
                SeoName = cuisineType.SeoName
            };
        }

        private RatingCore MapRating(Rating rating)
        {
            if (rating == null)
                return null;

            return new RatingCore()
            {
                Average = rating.Average,
                Count = rating.Count,
                StarRating = rating.StarRating
            };
        }
    }
}
