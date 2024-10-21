using BusinessObject.DTOS;
using BusinessObject.Models.Entity;

namespace Repository.IRepository
{
    public interface IBrandRepository
    {
        public List<Brand> GetBrands();
        public Task<List<BrandModel>> GetBrandList();
        public Task<bool> ChangeBrandStatus(int BrandId, bool Status);
        public Task<bool> InsertNewBrand(InsertBrandModel brand);
        public Task<bool> UpdateBrand(UpdateBrandModel brand);
    }
}
