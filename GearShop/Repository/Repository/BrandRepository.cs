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
    }
}
