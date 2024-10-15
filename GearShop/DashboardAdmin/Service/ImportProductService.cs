using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DashboardAdmin.Admin_APIEndPoint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DashboardAdmin.Service
{
    public class ImportProductService
    {
        private readonly HttpClient _client;

        public ImportProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> ImportProduct(ItemsControl cartItem, ObservableCollection<ProductModel> cartProducts)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var items in cartItem.Items)
            {
                if (items is ProductModel product)
                {
                    productModels.Add(product);
                }
            }

            ImportProductModel IRmodel = new ImportProductModel
            {
                DateImport = DateOnly.FromDateTime(DateTime.Now),
                Payment = cartProducts.Sum(p => p.TotalPrice),
                PersonChange = "Admin"
            };

            List<ReceiptProductModel> list = new List<ReceiptProductModel>();


            //Add Import Product
            var importProductJson = JsonSerializer.Serialize(IRmodel, options);
            var contentImportProduct = new StringContent(importProductJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Admin_APIEndPoint_ImportProduct.ADD_IMPORT_RECEIPT, contentImportProduct);

            var strData = await response.Content.ReadAsStringAsync();

            ImportProductModel _importProduct = JsonSerializer.Deserialize<ImportProductModel>(strData, options);

            int ID = _importProduct.ReceiptId;
            //-------------------

            foreach (var items in productModels)
            {
                ReceiptProductModel receipt = new ReceiptProductModel
                {
                    ImportProductId = ID,
                    ProId = items.ProId,
                    ProName = items.ProName,
                    Amount = items.ProQuan,
                    Price = items.ProPrice
                };
                list.Add(receipt);
            }

            var importReceiptJson = JsonSerializer.Serialize(list, options);
            var contentReceiptProduct = new StringContent(importReceiptJson, Encoding.UTF8, "application/json");
            response = await _client.PostAsync(Admin_APIEndPoint_ImportProduct.ADD_IMPORT_PRODUCT, contentReceiptProduct);
            strData = await response.Content.ReadAsStringAsync();

            var isSuccess = JsonSerializer.Deserialize<bool>(strData, options);

            if (isSuccess)
            {
                response = await _client.PutAsync(Admin_APIEndPoint_Product.ADD_QUANTITY_TO_PRODUCT, contentReceiptProduct);
                strData = await response.Content.ReadAsStringAsync();
                isSuccess = JsonSerializer.Deserialize<bool>(strData, options);

                if (isSuccess)
                {
                    return true;
                } else
                {
                    return false;
                }

            } else
            {
                return false;
            }

            return false;
        }

    }
}
