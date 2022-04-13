using System.Collections.Generic;

namespace JustEat.Operations.Restaurant.Api.CoreModels
{
    public class RestaurantCore
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public bool IsOpenNow { get; set; }

        public RatingCore Rating { get; set; }

        public IEnumerable<CuisineTypeCore> CuisineTypes { get; set; }
    }
}
