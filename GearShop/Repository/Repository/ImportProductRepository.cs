using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class ImportProductRepository : IImportProductRepository
    {
        public async Task<bool> AddReceiptProduct(List<ReceiptProductModel> list)
        => await ImportProductDAO.AddReceiptProduct(list);
        public async Task<ImportProductModel> CreateImportReceipt(ImportProductModel _ImportProduct)
        => await ImportProductDAO.CreateImportReceipt(_ImportProduct);
        public async Task<ImportProductModel> GetImportProduct(int ImportProductId)
        => await ImportProductDAO.GetImportProduct(ImportProductId);
        public async Task<List<ImportProductModel>> GetImportProductsList()
        => await ImportProductDAO.GetImportProductsList();
        public async Task<double> GetMoneySpent()
        => await ImportProductDAO.GetMoneySpent();
        public async Task<List<ReceiptProductModel>> GetReceiptProductsByID(int ImportProductId)
        => await ImportProductDAO.GetReceiptProductsByID(ImportProductId);
    }
}
