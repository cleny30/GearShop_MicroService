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
    public class ImportProductRepository : IImportProductRepository
    {
        public async Task<List<ImportProductModel>> GetImportProductsList()
        => await ImportProductDAO.GetImportProductsList();

        public async Task<double> GetMoneySpent()
        => await ImportProductDAO.GetMoneySpent();
    }
}
