using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAdmin
{
    public class Admin_APIEndPoint_Manager
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string CHECK_USERNAME_EXISTED = BASE_URL + "/gateway/managers/CheckUsernameExisted";

        public const string CHECK_MANAGER_EXISTED = BASE_URL + "/gateway/managers/CheckManagerExisted";
    }
}
