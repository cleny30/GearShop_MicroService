using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAdmin
{
    public class Admin_APIEndPoint
    {
        public const string BaseURL = "http://localhost:5000";

        public const string CheckUsernameExisted = BaseURL + "/gateway/managers/CheckUsernameExisted";

        public const string CheckManagerExisted = BaseURL + "/gateway/managers/CheckManagerExisted";
    }
}
