using BusinessObject.DTOS;
using DashboardAdmin.Admin_APIEndPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DashboardAdmin.Service
{
    public class ProductPageService
    {
        private readonly HttpClient _client;

        public ProductPageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ProductModel>> GetProductList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductModel>>(strData, options);
        }

        public async Task<List<BrandModel>> GetBrandList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_BRAND_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BrandModel>>(strData, options);
        }

        public async Task<List<CategoryModel>> GetCategoryList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_CATEGORY_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<CategoryModel>>(strData, options);
        }
    }
}
