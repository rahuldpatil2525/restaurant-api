namespace JustEat.Operations.Restaurant.Api.ResponseModels
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public string ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}
