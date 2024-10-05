using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static async Task<List<ProductModel>> GetProductListAdmin()
        {
            List<Product> products;
            try
            {
                var dbContext = new ProductContext();
                products = await dbContext.Products.ToListAsync();
                List<ProductModel> ProductModels = new List<ProductModel>();
                foreach (var product in products)
                {
                    ProductModel ProductModel = new ProductModel();
                    ProductModel.CopyProperties(product);
                    ProductModels.Add(ProductModel);
                }
                return ProductModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
