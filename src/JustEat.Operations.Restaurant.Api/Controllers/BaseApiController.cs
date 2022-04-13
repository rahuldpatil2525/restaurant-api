using JustEat.Operations.Restaurant.Api.Constants;
using JustEat.Operations.Restaurant.Api.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace JustEat.Operations.Restaurant.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected ActionResult ErrorResponse(string errorCode, string errorMessage)
        {
            if (errorCode == ErrorCodes.InternalServerError || errorCode == ErrorCodes.DependencyFailed)
                return InternalServerError(errorCode, errorMessage);

            return BadRequestResponse(errorCode, errorMessage);
        }

        protected ActionResult InternalServerError(string errorCode, string errorMessage)
        {
            var response = new ErrorResponse(errorCode, errorMessage);
            return StatusCode(500, response);
        }

        protected ActionResult BadRequestResponse(string errorCode, string errorMessage)
        {
            var response = new ErrorResponse(errorCode, errorMessage);
            return BadRequest(response);
        }
    }
}
