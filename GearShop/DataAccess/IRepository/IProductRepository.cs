using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public List<ProductImage> GetProductImages();
        public List<ProductAttribute> GetProductAttributes();
        public Task<List<ProductModel>> GetProductListAdmin();
        public Task<List<ProductData>> SearchProductsByName(string productName);
        public Task<string> GetNewProductID(int CatID);
        public Task<bool> InsertProduct(ProductData product);
        public Task<ProductModel> GetProductByID(string ProId);
    }
}
