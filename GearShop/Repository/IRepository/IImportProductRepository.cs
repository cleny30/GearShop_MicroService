using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IImportProductRepository
    {
        public Task<List<ImportProductModel>> GetImportProductsList();
        public Task<double> GetMoneySpent();
        public Task<ImportProductModel> CreateImportReceipt(ImportProductModel _ImportProduct);
        public Task<bool> AddReceiptProduct(List<ReceiptProductModel> list);
        public Task<ImportProductModel> GetImportProduct(int ImportProductId);
        public Task<List<ReceiptProductModel>> GetReceiptProductsByID(int ImportProductId);
    }
}
