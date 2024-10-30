using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IProductAttributeRepository
    {
        public Task<bool> AddProductAttribute(List<ProductAttributeModel> productAttributes);
        public Task<List<ProductAttributeModel>> GetProductAttributesByID(string ProId);
        public Task<bool> DeleteProductAttributeByID(string ProId);
    }
}
