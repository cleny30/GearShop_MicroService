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
    public class ImportProductRepository : IImportProductRepository
    {
        public async Task<List<ImportProductModel>> GetImportProductsList()
        => await ImportProductDAO.GetImportProductsList();

        public async Task<double> GetMoneySpent()
        => await ImportProductDAO.GetMoneySpent();
    }
}
