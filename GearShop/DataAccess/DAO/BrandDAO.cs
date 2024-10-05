using BusinessObject.Core;
using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BrandDAO
    {
        public static List<Brand> GetBrands()
        {
            var list = new List<Brand>();
            try
            {
                using (var context = new ProductContext())
                {
                    list = context.Brands.ToList();
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
