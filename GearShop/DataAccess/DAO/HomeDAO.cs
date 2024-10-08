using BusinessObject.Core;
using BusinessObject.DTOS;

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

            return productList.Take(8)
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

            return productList.Take(8)
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
