using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.Core.Constants;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class HomeDAO
    {
        private readonly ProductDAO productDAO;

        public HomeDAO(ProductDAO productDAO)
        {
            this.productDAO = productDAO;
        }

        public List<ProductData> GetMouseProducts()
        {
            var productList = ProductDAO.GetProducts();

            return productList.Where(p => p.CateId.Equals((int)CategoryType.Mouse) && p.IsAvailable)
                              .Take(8)
                              .Select(p => new ProductData
                              {
                                  ProId = p.ProId,
                                  CateId = p.CateId,
                                  BrandId = p.BrandId,
                                  ProName = p.ProName,
                                  ProQuan = p.ProQuan,
                                  ProPrice = p.ProPrice,
                                  ProDes = p.ProDes,
                                  Discount = p.Discount,
                                  IsAvailable = p.IsAvailable
                                  // Add other properties as needed
                              })
                              .ToList();
        }

        public List<ProductData> GetKeyboardProducts()
        {
            var productList = ProductDAO.GetProducts();

            return productList.Where(p => p.CateId.Equals((int)CategoryType.Keyboard) && p.IsAvailable)
                              .Take(8)
                              .Select(p => new ProductData
                              {
                                  ProId = p.ProId,
                                  CateId = p.CateId,
                                  BrandId = p.BrandId,
                                  ProName = p.ProName,
                                  ProQuan = p.ProQuan,
                                  ProPrice = p.ProPrice,
                                  ProDes = p.ProDes,
                                  Discount = p.Discount,
                                  IsAvailable = p.IsAvailable
                                  // Add other properties as needed
                              })
                              .ToList();
        }

        public List<ProductData> HomeGetData()
        {
            List<ProductData> productModels = new List<ProductData>();

            try
            {
                using var dbContext = new ProductContext();
                var products = dbContext.Products.ToList();

                foreach (var product in products)
                {
                    ProductData productModel = new ProductData
                    {
                        ProId = product.ProId,
                        CateId = product.CateId,
                        BrandId = product.BrandId,
                        ProName = product.ProName,
                        ProQuan = product.ProQuan,
                        ProPrice = product.ProPrice,
                        ProDes = product.ProDes,
                        Discount = product.Discount,
                        IsAvailable = product.IsAvailable
                        // Add other properties as needed
                    };
                    productModels.Add(productModel);
                }

                return productModels;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
        }
    }
}
