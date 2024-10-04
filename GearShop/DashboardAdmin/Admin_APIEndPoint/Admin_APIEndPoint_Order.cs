using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAdmin.Admin_APIEndPoint
{
     public class Admin_APIEndPoint_Order
    {
        public const string BaseURL = "http://localhost:5000";

        public const string GetCompletedOrder = BaseURL;

        public const string GetIncome = BaseURL;

        public const string GetRevenue = BaseURL;

        public const string GetAllOrderDetailList = BaseURL;

        public const string GetTop10Customer = BaseURL;
    }
}
