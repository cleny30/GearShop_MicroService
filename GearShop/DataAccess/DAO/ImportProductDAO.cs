using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class ImportProductDAO
    {
        private static DateTime currentDate = DateTime.Now;
        public static async Task<List<ImportProductModel>> GetImportProductsList()
        {
            List<ImportProduct> list = new List<ImportProduct>();
            try
            {
                var dbContext = new ImportProductContext();
                list = dbContext.ImportProducts.ToList();
                List<ImportProductModel> _list = new List<ImportProductModel>();
                foreach (var item in list)
                {
                    ImportProductModel model = new ImportProductModel();
                    model.CopyProperties(item);
                    _list.Add(model);
                }
                return _list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<double> GetMoneySpent()
        {
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;
            List<ImportProductModel> receipts = await GetImportProductsList();
            var spentList = receipts
                .Where(ir => ir.DateImport != null && ir.DateImport.Year == currentYear && ir.DateImport.Month == currentMonth)
                .Select(ir => ir.Payment)
                .ToList();
            return spentList.Sum(); 
        }

        public static async Task<ImportProductModel> CreateImportReceipt(ImportProductModel _ImportProduct)
        {
            try
            {
                var dbContext = new ImportProductContext();
                ImportProduct _receipt = new ImportProduct
                { 
                    DateImport = _ImportProduct.DateImport,
                    Payment = _ImportProduct.Payment,
                    PersonInCharge = _ImportProduct.PersonInCharge,
                };

                await dbContext.ImportProducts.AddAsync(_receipt);
                int check = await dbContext.SaveChangesAsync();

                if (check > 0)
                {
                    _ImportProduct.ImportProductId = _receipt.ImportProductId;
                    return _ImportProduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return null;
            }
        }   

        public static async Task<bool> AddReceiptProduct(List<ReceiptProductModel> list)
        {
            try
            {
                using (var dbContext = new ImportProductContext())
                {
                    foreach (var item in list)
                    {
                        var receiptProduct = new ReceiptProduct
                        {
                            Amount = item.Amount,
                            Price = item.Price,
                            ProId = item.ProId,
                            ProName = item.ProName,
                            ImportProductId = item.ImportProductId
                        };
                        await dbContext.AddAsync(receiptProduct);
                    }

                    int result = await dbContext.SaveChangesAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return false;
            }
        }

        public static async Task<ImportProductModel> GetImportProduct(int ImportProductId)
        {
            ImportProduct _ImportProduct = new ImportProduct();
            try
            {
                var dbContext = new ImportProductContext();
                _ImportProduct = await dbContext.ImportProducts.FirstOrDefaultAsync(i => i.ImportProductId == ImportProductId);
                List<ImportProductModel> _list = new List<ImportProductModel>();
                ImportProductModel model = new ImportProductModel();
                model.CopyProperties(_ImportProduct);
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<List<ReceiptProductModel>> GetReceiptProductsByID(int ImportProductId)
        {
            List<ReceiptProduct> list = new List<ReceiptProduct>();
            try
            {
                var dbContext = new ImportProductContext();
                list = await dbContext.ReceiptProducts.Where(i => i.ImportProductId == ImportProductId).ToListAsync();
                List<ReceiptProductModel> _list = new List<ReceiptProductModel>();
                foreach (var item in list)
                {
                    ReceiptProductModel model = new ReceiptProductModel();
                    model.CopyProperties(item);
                    _list.Add(model);
                }
                return _list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
