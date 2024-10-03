using BusinessObject.Core;
using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ManagerDAO
    {
        public static List<Manager> GetManagers()
        {
            var list = new List<Manager>();
            try
            {
                using (var context = new ManagerContext())
                {
                    list = context.Managers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
    }
}
