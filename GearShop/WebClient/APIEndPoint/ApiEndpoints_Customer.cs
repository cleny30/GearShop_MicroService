namespace WebClient.APIEndPoint
{
    public static class ApiEndpoints_Customer
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers";

        public const string UPDATE_CUSTOMER_BY_USERNAME = $"{BASE_URL}/gateway/customers/{{0}}";

        public const string GET_CUSTOMER_BY_USERNAME_LOGIN = $"{BASE_URL}/gateway/customers/GetCustomerByUsername/{{0}}/{{1}}";

        public const string CHANGE_PASSWORD = $"{BASE_URL}/gateway/customers/ChangePassword";

        public const string CHECK_MAIL = $"{BASE_URL}/gateway/customers/CheckMail/{{0}}"; 

        public const string FORGET_PASSWORD = $"{BASE_URL}/gateway/customers/ForgetPassword/{{0}}/{{1}}";

        public const string GET_ALL_ADDRESS = $"{BASE_URL}/gateway/addresses/GetAddress?username={{0}}";
      
        public const string ADD_ADDRESS = $"{BASE_URL}/gateway/addresses";

        public const string UPDATE_ADDRESS = $"{BASE_URL}/gateway/addresses";

        public const string DELETE_ADDRESS = $"{BASE_URL}/gateway/addresses";
    }
}
