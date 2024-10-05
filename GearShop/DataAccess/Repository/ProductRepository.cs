using BusinessObject.Models.Entity;
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
    }
}
