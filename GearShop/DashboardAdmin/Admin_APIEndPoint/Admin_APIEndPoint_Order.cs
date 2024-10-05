using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
