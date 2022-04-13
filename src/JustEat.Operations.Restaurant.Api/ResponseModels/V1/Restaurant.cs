using System.Collections.Generic;

namespace JustEat.Operations.Restaurant.Api.ResponseModels.V1
{
    public class Restaurant
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public bool IsOpenNow { get; set; }

        public Rating Rating { get; set; }

        public IEnumerable<CuisineType> CuisineTypes { get; set; }
    }
}
