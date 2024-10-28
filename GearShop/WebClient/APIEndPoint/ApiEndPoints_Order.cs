namespace WebClient.APIEndPoint
{
    public class ApiEndPoints_Order
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_NEW_ORDERID = $"{BASE_URL}/gateway/orders/GetNewOrderID";
        public const string ADD_ORDER = $"{BASE_URL}/gateway/orders/AddOrderModel";
        public const string ADD_ORDER_DETAILS = $"{BASE_URL}/gateway/orders/AddOrderDetails";
    }
}
