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
    }
}
