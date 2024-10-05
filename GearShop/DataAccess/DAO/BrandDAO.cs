using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BrandDAO
    {
        public static async Task<List<BrandModel>> GetBrandList()
        {
            List<Brand> brands;
            try
            {
                using (var dbContext = new ProductContext())
                {
                    brands = await dbContext.Brands.ToListAsync(); // Use ToListAsync for asynchronous operation
                }

                List<BrandModel> brandModels = new List<BrandModel>();

                foreach (var item in brands)
                {
                    BrandModel brandModel = new BrandModel();
                    brandModel.CopyProperties(item);
                    brandModels.Add(brandModel);
                }
                return brandModels;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return null;
            }
        }
    }
}
