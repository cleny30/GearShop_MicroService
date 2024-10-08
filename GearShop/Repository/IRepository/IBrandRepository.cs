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
    }
}
