using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAdmin.Admin_APIEndPoint
{
    public class Admin_APIEndPoint_ImportProduct
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string ADD_IMPORT_RECEIPT = BASE_URL + "/gateway/ImportProducts/AddImportReceipt";

        public const string ADD_IMPORT_PRODUCT = BASE_URL + "/gateway/ImportProducts/AddImportProduct";

    }
}
