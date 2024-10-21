using BusinessObject.DTOS;
using DashboardAdmin.Admin_APIEndPoint;
using Repository.Core.Cloudiary;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DashboardAdmin.Service
{
    public class BrandPageService
    {
        private readonly HttpClient _client;

        public BrandPageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> ChangeBrandStatus(int BrandId, bool Status)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.PutAsync($"{Admin_APIEndPoint_Product.CHANGE_BRAND_STATUS}?BrandId={BrandId}&Status={Status}", null);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> InsertNewBrand(string SelectedBrandLogo, string BrandName)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string cloudinaryLink = await cloud.Upload(SelectedBrandLogo, "Brands");
            string BrandImage = cloudinaryLink;
            InsertBrandModel model = new InsertBrandModel
            {
                BrandName = BrandName,
                BrandLogo = BrandImage,
                IsAvailable = false
            };

            var brandJson = JsonSerializer.Serialize(model, options);
            var contentBrand = new StringContent(brandJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Admin_APIEndPoint_Product.INSERT_NEW_BRAND, contentBrand);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }

        public async Task<bool> UpdateBrand(string SelectedBrandLogo, string BrandName, string ChangedBrandLogo, int BrandId)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string BrandImage = ChangedBrandLogo;
            if (!ChangedBrandLogo.Equals(SelectedBrandLogo))
            {
                bool delete = await cloud.Delete(ChangedBrandLogo);
                string cloudinaryLink = await cloud.Upload(SelectedBrandLogo, "Brand");
                BrandImage = cloudinaryLink;
            }

            UpdateBrandModel _brand = new UpdateBrandModel
            {
                BrandId = BrandId,
                BrandName = BrandName,
                BrandLogo = BrandImage
            };

            var brandJson = JsonSerializer.Serialize(_brand, options);
            var contentBrand = new StringContent(brandJson, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(Admin_APIEndPoint_Product.UPDATE_BRAND, contentBrand);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }
    }
}
