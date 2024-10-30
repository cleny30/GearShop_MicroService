using BusinessObject.DTOS;
using DashboardAdmin.Admin_APIEndPoint;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DashboardAdmin.Service
{
    public class CategoryPageService
    {
        private readonly HttpClient _client;

        public CategoryPageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> IsKeywordExisted(string keyword)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync($"{Admin_APIEndPoint_Product.IS_KEYWORD_EXISTED}?keyword={keyword}");
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> ChangeCategoryStatus(int CateId, bool Status)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.PutAsync($"{Admin_APIEndPoint_Product.CHANGE_CATEGORY_STATUS}?CateId={CateId}&Status={Status}", null);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> InsertNewCategory(string CateName, string Keyword)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            InsertCategoryModel category = new InsertCategoryModel
            {
                CateName = CateName,
                Keyword = Keyword,
                IsAvailable = false
            };

            var cateJson = JsonSerializer.Serialize(category, options);
            var contentCate = new StringContent(cateJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Admin_APIEndPoint_Product.INSERT_NEW_CATEGORY, contentCate);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> UpdateCategory(int CateId, string CateName)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            UpdateCategoryModel category = new UpdateCategoryModel
            {
                CateId = CateId,
                CateName = CateName
            };

            var cateJson = JsonSerializer.Serialize(category, options);
            var contentCate = new StringContent(cateJson, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(Admin_APIEndPoint_Product.UPDATE_CATEGORY, contentCate);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }
    }
}
