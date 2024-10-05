using BusinessObject.Core;
using BusinessObject.Models.Entity;
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
    }
}
