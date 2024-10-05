using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IImportProductRepository
    {
        public Task<List<ImportProductModel>> GetImportProductsList();
        public Task<double> GetMoneySpent();
    }
}
