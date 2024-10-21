using BusinessObject.DTOS;
using DashboardAdmin.Admin_APIEndPoint;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

            string managerPath = "ManagerUsername.json";
            string Fullname = "Admin";
            if (File.Exists(managerPath))
            {
                try
                {
                    // Read the JSON content from the file
                    string jsonContent = File.ReadAllText(managerPath);

                    // Deserialize the JSON content to an anonymous object
                    var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonContent);

                    // Extract the Username
                    Fullname = jsonObject.GetProperty("Username").GetString(); ;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                }
            }

            ImportProductModel IRmodel = new ImportProductModel
            {
                DateImport = DateOnly.FromDateTime(DateTime.Now),
                Payment = cartProducts.Sum(p => p.TotalPrice),
                PersonInCharge = Fullname
            };

            List<ReceiptProductModel> list = new List<ReceiptProductModel>();


            //Add Import Product
            var importProductJson = JsonSerializer.Serialize(IRmodel, options);
            var contentImportProduct = new StringContent(importProductJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Admin_APIEndPoint_ImportProduct.ADD_IMPORT_RECEIPT, contentImportProduct);

            var strData = await response.Content.ReadAsStringAsync();

            ImportProductModel _importProduct = JsonSerializer.Deserialize<ImportProductModel>(strData, options);

            int ID = _importProduct.ImportProductId;
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


        public async Task<List<ImportProductModel>> GetImportProductList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync(Admin_APIEndPoint_ImportProduct.GET_IMPORT_PRODUCT_LIST);

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ImportProductModel>>(strData, options);
        }

        public async Task<ImportProductModel> GetImportProductById(int ImportProductId)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync($"{Admin_APIEndPoint_ImportProduct.GET_IMPORT_PRODUCT_BY_ID}?ImportProductId={ImportProductId}");

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ImportProductModel>(strData, options);
        }

        public async Task<List<ReceiptProductModel>> GetReceiptProductById(int ImportProductId)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync($"{Admin_APIEndPoint_ImportProduct.GET_RECEIPT_PRODUCT_BY_ID}?ImportProductId={ImportProductId}");

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ReceiptProductModel>>(strData, options);
        }

    }
}
