
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

        public static async Task<bool> ChangeBrandStatus(int BrandId, bool Status)
        {
            try
            {
                using (var dbContext = new ProductContext())
                {
                    Brand brand = dbContext.Brands.FirstOrDefault(b => b.BrandId == BrandId);
                    if (brand == null)
                    {
                        // Optionally log that the brand was not found
                        return false;
                    }
                    brand.IsAvailable = Status;
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> InsertNewBrand(InsertBrandModel brand)
        {
            try
            {
                Brand _brand = new Brand();
                using (var dbContext = new ProductContext())
                {
                    var lastBrand = await dbContext.Brands
                    .OrderByDescending(b => b.BrandId)
                    .FirstOrDefaultAsync();

                    int BrandId = lastBrand.BrandId + 1;

                    brand.BrandId = BrandId;   

                    _brand.CopyProperties(brand);
                    await dbContext.AddAsync(_brand);  
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> UpdateBrand(UpdateBrandModel brand)
        {
            try
            {
                if (brand == null)
                {
                    // Optionally log that the input brand model is null
                    return false;
                }
                using (var dbContext = new ProductContext())
                {
                    Brand _brand = dbContext.Brands.FirstOrDefault(b => b.BrandId == brand.BrandId);
                    if (_brand == null)
                    {
                        // Optionally log that the brand was not found
                        return false;
                    }

                    _brand.BrandName = brand.BrandName;
                    _brand.BrandLogo = brand.BrandLogo;
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
