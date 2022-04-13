using System.Threading;
using System.Threading.Tasks;
using JustEat.Operations.Restaurant.Api.Builders;
using JustEat.Operations.Restaurant.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace JustEat.Operations.Restaurant.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : BaseApiController
    {
        private readonly IRestaurantGateway _restaurantGateway;
        private readonly IRestaurantResponseBuilder _restaurantResponseBuilder;

        public RestaurantController(IRestaurantGateway restaurantGateway, IRestaurantResponseBuilder restaurantResponseBuilder)
        {
            _restaurantGateway = restaurantGateway;
            _restaurantResponseBuilder = restaurantResponseBuilder;
        }

        [HttpGet]
        [Route("v1/postcode/{postCode}")]
        public async Task<ActionResult> GetAsync(string postCode, CancellationToken ct)
        {
            var restaurantResult = await _restaurantGateway.GetRestaurantsAsync(postCode, ct);

            if (restaurantResult.HasError)
            {
                return ErrorResponse(restaurantResult.ErrorCode, restaurantResult.ErrorMessage);
            }

            var response = _restaurantResponseBuilder.Build(restaurantResult);

            return Ok(response);
        }
    }
}
