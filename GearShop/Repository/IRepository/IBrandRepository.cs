using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
