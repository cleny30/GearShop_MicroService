using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

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
