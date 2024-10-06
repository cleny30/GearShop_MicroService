namespace WebClient.APIEndPoint
{
    public static class ApiEndpoints_Customer
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers";

        public const string UPDATE_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers";

    }
}
