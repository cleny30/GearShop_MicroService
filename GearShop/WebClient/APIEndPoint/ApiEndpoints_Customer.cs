namespace WebClient.APIEndPoint
{
    public static class ApiEndpoints_Customer
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers";

        public const string UPDATE_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers/{{0}}";

        public const string GET_CUSTOMER_BY_USERNAME_LOGIN = $"{BASE_URL}/gateway/customers/GetCustomerByUsername/{{0}}/{{1}}";
        public const string CHANGE_PASSWORD = $"{BASE_URL}/gateway/customers/ChangePassword";


        public const string GET_ALL_ADDRESS = $"{BASE_URL}/gateway/customers/GetAddress?username={{0}}";
      
        
    }
}
