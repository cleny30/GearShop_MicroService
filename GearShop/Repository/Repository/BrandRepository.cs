using BusinessObject.Models.Entity;
using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public List<Brand> GetBrands()=>BrandDAO.GetBrands();
        public async Task<List<BrandModel>> GetBrandList()
        => await BrandDAO.GetBrandList();
        public async Task<bool> ChangeBrandStatus(int BrandId, bool Status)
        => await BrandDAO.ChangeBrandStatus(BrandId, Status);
        public async Task<bool> InsertNewBrand(InsertBrandModel brand)
        => await BrandDAO.InsertNewBrand(brand);
        public async Task<bool> UpdateBrand(UpdateBrandModel brand)
        => await BrandDAO.UpdateBrand(brand);
    }
}
