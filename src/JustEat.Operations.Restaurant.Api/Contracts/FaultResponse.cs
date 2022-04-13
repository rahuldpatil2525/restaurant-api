using System.Collections.Generic;

namespace JustEat.Operations.Restaurant.Api.Contracts
{
    public class FaultResponse
    {
        public string TraceId { get; set; }
        public string FaultId { get; set; }

        public IList<RestaurantApiErrorResponse> Errors { get; set; }
    }
}
