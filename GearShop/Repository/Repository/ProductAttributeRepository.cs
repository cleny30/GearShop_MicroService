using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        public async Task<bool> AddProductAttribute(List<ProductAttributeModel> productAttributes)
        => await ProductAttributeDAO.AddProductAttribute(productAttributes);
        public async Task<bool> DeleteProductAttributeByID(string ProId)
        => await ProductAttributeDAO.DeleteProductAttributeByID(ProId);
        public async Task<List<ProductAttributeModel>> GetProductAttributesByID(string ProId)
        => await ProductAttributeDAO.GetProductAttributesByID(ProId);
    }
}
