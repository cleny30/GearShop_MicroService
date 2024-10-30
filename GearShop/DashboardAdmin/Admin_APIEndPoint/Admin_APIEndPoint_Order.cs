
namespace DashboardAdmin.Admin_APIEndPoint
{
     public class Admin_APIEndPoint_Order
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_ORDER_LIST = BASE_URL + "/gateway/orders/GetOrderList";

        public const string GET_COMPLETED_ORDER = BASE_URL + "/gateway/orders/GetCompletedOrder";

        public const string GET_INCOME = BASE_URL + "/gateway/orders/GetIncome";

        public const string GET_REVENUE = BASE_URL + "/gateway/orders/GetRevenue";

        public const string GET_ALL_ORDER_DETAIL_LIST = BASE_URL + "/gateway/orders/GetAllOrderDetailList";

        public const string GET_TOP_10_CUSTOMERS = BASE_URL + "/gateway/orders/GetTop10Customers";

        public const string GET_ORDER_BY_ID = BASE_URL + "/gateway/orders/GetOrderByID";

        public const string GET_ORDER_DETAIL_BY_ORDER_ID = BASE_URL + "/gateway/orders/GetOrderDetailByOrderID";

        public const string CHANGE_ORDER_STATUS = BASE_URL + "/gateway/orders/ChangeOrderStatus";
    }
}
