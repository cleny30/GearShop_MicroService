using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using (var context = new ProductContext())
                {
                    list = context.Products.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static List<ProductAttribute> GetProductAttributes()
        {
            var list = new List<ProductAttribute>();
            try
            {
                using (var context = new ProductContext())
                {
                    list = context.ProductAttributes.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static List<ProductImage> GetProductImages()
        {
            var list = new List<ProductImage>();
            try
            {
                using (var context = new ProductContext())
                {
                    list = context.ProductImages.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

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

        public static async Task<List<ProductData>> SearchProductByName(string productName)
        {
            List<Product> products = new List<Product>();

            List<ProductData> model = new List<ProductData>();
            try
            {
                var dbContext = new ProductContext();
                products = await dbContext.Products.Where(p => p.ProName.Contains(productName)).ToListAsync();
                if (products != null)
                {
                    foreach (var product in products)
                    {


                        model.Add(new ProductData
                        {
                            ProId = product.ProId,
                            ProName = product.ProName,
                            Discount = product.Discount,
                            IsAvailable = product.IsAvailable,
                            ProPrice = product.ProPrice
                        });
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    
    }
}
