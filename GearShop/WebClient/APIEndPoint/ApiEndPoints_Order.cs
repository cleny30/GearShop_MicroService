namespace WebClient.APIEndPoint
{
    public class ApiEndPoints_Order
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_NEW_ORDERID = $"{BASE_URL}/gateway/orders/GetNewOrderID";
        public const string ADD_ORDER = $"{BASE_URL}/gateway/orders/AddOrderModel";
        public const string ADD_ORDER_DETAILS = $"{BASE_URL}/gateway/orders/AddOrderDetails";
        public const string GET_ORDER_BY_USERNAME = $"{BASE_URL}/gateway/orders/GetOrderByUsername";
        public const string GET_ORDER_BY_ID = $"{BASE_URL}/gateway/orders/GetOrderByID";
        public const string GET_ORDER_DETAILS = $"{BASE_URL}/gateway/orders/GetOrderDetailByOrderID";
        public const string CHANGE_ORDER_STATUS = BASE_URL + "/gateway/orders/ChangeOrderStatus";
    }
}
