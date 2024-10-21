using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IProductImageRepository
    {
        public Task<bool> AddImageOfSpecificProduct(List<ProductImageModel> imageLink);

        public Task<List<ProductImageModel>> GetProductImagesByID(string ProId);

        public Task<bool> RemoveImageByID(List<ProductImageModel> imageLink);
    }
}
