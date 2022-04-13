namespace JustEat.Operations.Restaurant.Api.Contracts
{
    public class RestaurantApiErrorResponse
    {
        public string Message { get; set; }

        public string Description { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionType { get; set; }

        public string StackTrace { get; set; }
    }
}
