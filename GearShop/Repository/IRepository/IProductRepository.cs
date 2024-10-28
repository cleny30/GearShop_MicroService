using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
namespace Repository.IRepository
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
        public Task<bool> UpdateProduct(ProductData product);
        public Task<bool> ChangeProductStatus(string ProId, bool Status);
        public Task<bool> AddQuantityToProduct(List<ReceiptProductModel> products);
        public Task<bool> UpdateQuantityToProduct(List<ProductData> products);
    }
}
