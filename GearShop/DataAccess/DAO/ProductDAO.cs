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
            List<ProductData> productModels = new List<ProductData>();

            try
            {
                var dbContext = new ProductContext();
                products = await dbContext.Products
                    .Where(p => p.ProName.Contains(productName))
                    .ToListAsync();

                foreach (var product in products)
                {
                    ProductData productModel = new ProductData();
                    productModel.CopyProperties(product);
                    productModels.Add(productModel);
                }

                return productModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<string> GetNewProductID(int CatID)
        {
            try
            {
                using (var dbContext = new ProductContext())
                {
                    var lastProductId = await dbContext.Products
                        .Where(p => p.CateId == CatID)
                        .OrderByDescending(p => p.ProId)
                        .Select(p => p.ProId)
                        .FirstOrDefaultAsync();

                    if (lastProductId == null)
                    {
                        var categoryKeyword = await dbContext.Categorys
                            .Where(c => c.CateId == CatID)
                            .Select(c => c.Keyword)
                            .FirstOrDefaultAsync();

                        if (categoryKeyword != null)
                        {
                            string newProductId = $"{categoryKeyword}001";
                            return newProductId;
                        }
                    }
                    else
                    {
                        string numericPart = lastProductId.Substring(2);
                        int currentNumber = int.Parse(numericPart);
                        int newNumber = currentNumber + 1;
                        string newProductId = $"{lastProductId.Substring(0, 2)}{newNumber:D3}";
                        return newProductId;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You might want to rethrow the exception here for handling further up the call stack.
            }

            // Return null only if an exception occurs or no product ID is found
            return null;
        }

        public static async Task<bool> InsertProduct(ProductData product)
        {
            Product _product = new Product
            {
                ProId = product.ProId,
                ProName = product.ProName,
                BrandId = product.BrandId,
                CateId = product.CateId,
                Discount = product.Discount,
                ProDes = product.ProDes,
                ProPrice = product.ProPrice,
                IsAvailable = true,
                ProQuan = 0
            };
            try
            {
                var dbContext = new ProductContext();
                dbContext.Products.Add(_product);
                await dbContext.SaveChangesAsync();              
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
