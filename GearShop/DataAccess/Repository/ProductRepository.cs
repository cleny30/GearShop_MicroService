using BusinessObject.Models.Entity;
using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<ProductAttribute> GetProductAttributes() => ProductDAO.GetProductAttributes();

        public List<ProductImage> GetProductImages() => ProductDAO.GetProductImages();

        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public async Task<List<ProductModel>> GetProductListAdmin()
        => await ProductDAO.GetProductListAdmin();

        public async Task<List<ProductData>> SearchProductsByName(string productName)
        => await ProductDAO.SearchProductByName(productName);
    }
}
