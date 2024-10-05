using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public async Task<List<BrandModel>> GetBrandList()
        => await BrandDAO.GetBrandList();
    }
}
