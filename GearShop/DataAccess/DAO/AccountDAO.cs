using BusinessObject.Core;
using BusinessObject.DTOS;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        public static async Task<LoginAccountModel> ClientLogin(string username, string pass)
        {
            using (var context = new CustomerContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Username == username && c.Password == pass);
                if (customer != null)
                {

                    var loginAccount = new LoginAccountModel();
                    loginAccount.CopyProperties(customer);

                    return loginAccount;
                }
            }
            return null;

        }

    }
}
