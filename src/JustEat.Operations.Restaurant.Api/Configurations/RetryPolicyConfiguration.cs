namespace JustEat.Operations.Restaurant.Api.Configurations
{
    public interface IRetryPolicyConfiguration
    {
        int RetryAttempts { get; set; }
        int RetryInterval { get; set; }
    }

    public class RetryPolicyConfiguration : IRetryPolicyConfiguration
    {
        public int RetryAttempts { get; set; }
        public int RetryInterval { get; set; }
    }

    public class RestaurantApiConfiguration
    {
        public string BaseUrl { get; set; }
    }
}
