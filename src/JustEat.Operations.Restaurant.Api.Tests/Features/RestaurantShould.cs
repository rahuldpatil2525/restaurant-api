using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using JustEat.Operations.Restaurant.Api.Constants;
using JustEat.Operations.Restaurant.Api.ResponseModels;
using JustEat.Operations.Restaurant.Api.ResponseModels.V1;
using JustEat.Operations.Restaurant.Api.Tests.Builders;
using JustEat.Operations.Restaurant.Api.Tests.Factories;
using Xunit;

namespace JustEat.Operations.Restaurant.Api.Tests.Features
{
    public class RestaurantShould : IClassFixture<RestaurantApiFactory>
    {
        private readonly RestaurantApiFactory _factory;
        
        public RestaurantShould(RestaurantApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Return_List_Of_Restaurant_When_Pass_Valid_Postcode()
        {
            var apiResponse = RestaurantApiResponseBuilder.InMemory.CreateDefault();

            _factory.SetResponse(HttpStatusCode.OK, JsonSerializer.Serialize(apiResponse));

            var client = _factory.CreateClient();

            var httpResponse = await client.GetAsync("/api/Restaurant/v1/ne95ys");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            httpResponse.Content.Should().NotBeNull();

            var content =await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = JsonSerializer.Deserialize<RestaurantResponse>(content, new JsonSerializerOptions() 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            response.Count.Should().Be(1);
            response.Restaurants.Count.Should().Be(1);
        }

        [Fact]
        public async Task Return_Error_Response_When_Restaurant_Api_Request_Is_Failed_with_ToManyRequests()
        {
            _factory.SetResponse(HttpStatusCode.TooManyRequests, "{\"Fault\":{\"TraceId\":\"0HLOCKDKQPKIU\",\"FaultId\":\"72d7036d - 990a - 4f84 - 9efa - ef5f40f6044b\",\"Errors\":[{\"Description\":\"Couldn't complete request\"}]}}");

            var client = _factory.CreateClient();

            var httpResponse = await client.GetAsync("/api/Restaurant/v1/ne95ys");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            httpResponse.Content.Should().NotBeNull();

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = JsonSerializer.Deserialize<ErrorResponse>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            response.ErrorMessage.Should().Be("Couldn't complete request");
            response.ErrorCode.Should().Be(ErrorCodes.InternalServerError);
        }

        [Fact]
        public async Task Return_Error_Response_When_Restaurant_Api_Request_Is_Failed_with_BadRequests()
        {
            _factory.SetResponse(HttpStatusCode.BadRequest, "{\"ModelState\":{\"Postcode\":[\"Invalid Postcode.\"]},\"Message\":\"The request is invalid.\"}");

            var client = _factory.CreateClient();

            var httpResponse = await client.GetAsync("/api/Restaurant/v1/ne95ys");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().NotBeNull();

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = JsonSerializer.Deserialize<ErrorResponse>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            response.ErrorMessage.Should().Be("The request is invalid.");
            response.ErrorCode.Should().Be(ErrorCodes.BadRequest);
        }

        [Fact]
        public async Task Return_Error_Response_When_Restaurant_Api_Request_Is_Failed_with_InternalError()
        {
            _factory.SetResponse(HttpStatusCode.InternalServerError, "{\"ExceptionMessage\":\"Object reference not set to an instance of an object.\",\"Message\":\"An error has occurred.\",\"ExceptionType\":\"System.NullReferenceException\",\"StackTrace\":\"   at JE.SearchOrchestrator.Controllers.Filters.CacheControlFilter.OnActionExecuted(HttpActionExecutedContext actionExecutedContext)End of stack\"}");

            var client = _factory.CreateClient();

            var httpResponse = await client.GetAsync("/api/Restaurant/v1/ne95ys");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            httpResponse.Content.Should().NotBeNull();

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = JsonSerializer.Deserialize<ErrorResponse>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            response.ErrorMessage.Should().Be("An error has occurred."); 
            response.ErrorCode.Should().Be(ErrorCodes.InternalServerError);
        }
    }
}
