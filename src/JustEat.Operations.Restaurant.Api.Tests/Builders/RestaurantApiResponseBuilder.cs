using System.Collections.Generic;
using JustEat.Operations.Restaurant.Api.Contracts;

namespace JustEat.Operations.Restaurant.Api.Tests.Builders
{
    public class RestaurantApiResponseBuilder
    {
        public static RestaurantApiResponseBuilder InMemory => new RestaurantApiResponseBuilder();

        public RestaurantApiResponse CreateDefault()
        {
            return new RestaurantApiResponse()
            {
                Restaurants = new List<Contracts.Restaurant>()
                {
                    new Contracts.Restaurant()
                    {
                        City="City",
                        Name="Restaurant 1",
                        Postcode="PostCode",
                        Rating=new Rating()
                        {
                            Average=4.5,
                            Count=10,
                            StarRating=4.5
                        },
                        CuisineTypes=new List<Contracts.CuisineType>()
                        {
                            new CuisineType()
                            {
                                Name="British",
                                SeoName="British"
                            }
                        }
                    }
                },
                MetaData = new MetaData()
                {
                    ResultCount = 1
                }
            };
        }
    }
}
