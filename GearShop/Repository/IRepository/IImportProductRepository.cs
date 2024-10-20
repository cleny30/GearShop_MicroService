using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IImportProductRepository
    {
        public Task<List<ImportProductModel>> GetImportProductsList();
        public Task<double> GetMoneySpent();
        public Task<ImportProductModel> CreateImportReceipt(ImportProductModel _ImportProduct);
        public Task<bool> AddReceiptProduct(List<ReceiptProductModel> list);
    }
}
