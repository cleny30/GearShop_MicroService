using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
