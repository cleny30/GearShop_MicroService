using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;

namespace ProductService.SubService
{
    public class ProductService
    {
        public List<ProductData> GetProducts(List<Product> pro, List<ProductImage> imgs, List<ProductAttribute> att)
        {
            List<ProductData> products = new List<ProductData>();

            foreach (var item in pro)
            {
                ProductData product = new ProductData();
                product.CopyProperties(item);
                products.Add(product);
            }

            foreach (ProductData product in products)
            {
                product.ProImg = imgs
                       .Where(img => img.ProId == product.ProId)
                       .Select(img => img.ProImg)
                       .ToList();
                var attributes = att
                        .Where(attribute => attribute.ProId == product.ProId)
           .GroupBy(attribute => attribute.Feature)
           .ToDictionary(group => group.Key, group => group.First().Description); // Lấy Description đầu tiên

                product.ProAttribute = attributes;
            }
            return products;
        }
    }
}
