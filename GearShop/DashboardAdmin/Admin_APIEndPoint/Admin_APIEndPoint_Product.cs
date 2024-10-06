using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAdmin.Admin_APIEndPoint
{
    public class Admin_APIEndPoint_Product
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_PRODUCT_LIST = BASE_URL + "/gateway/products/GetProductList";

        public const string GET_PRODUCT_LIST_WITHOUT_BRAND_AND_CATEGORY = BASE_URL + "/gateway/products/GetProductListWithoutBrandAndCategory";

        public const string GET_BRAND_LIST = BASE_URL + "/gateway/Brands";

        public const string GET_CATEGORY_LIST = BASE_URL + "/gateway/Categories";

    }
}
