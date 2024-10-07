using BusinessObject.DTOS;
using System.Text.Json;
using WebClient.APIEndPoint;

namespace WebClient.Service
{
    public class HeaderService
    {
        private readonly HttpClient _httpClient;

        public HeaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HeaderModel> GetDataAsync(string? username)
        {
            // Thực hiện các yêu cầu HTTP để lấy danh sách sản phẩm, thương hiệu và danh mục
            var productList = await GetProductListAsync();
            var brandList = await GetBrandListAsync();
            var categoryList = await GetCategoryListAsync();

            // Tính toán số lượng sản phẩm theo thương hiệu
            foreach (var brand in brandList)
            {
                brand.quantity = productList.Count(product => product.BrandId == brand.BrandId);
            }

            // Tính toán số lượng sản phẩm theo danh mục
            foreach (var category in categoryList)
            {
                category.quantity = productList.Count(product => product.CateId == category.CateId);
            }

            // Tạo và điền dữ liệu vào HeaderModel
            var headerModel = new HeaderModel
            {
                brand = brandList,
                category = categoryList,
            };

            return headerModel;
        }

        // Hàm lấy danh sách sản phẩm từ API
        private async Task<List<ProductModel>> GetProductListAsync()
        {
            var response = await _httpClient.GetAsync(ApiEndpoints_Product.GET_HOME_PRODUCTS);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var productList = JsonSerializer.Deserialize<List<ProductModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return productList ?? new List<ProductModel>(); // Nếu null, trả về danh sách rỗng
            }

            return new List<ProductModel>(); // Nếu API thất bại, trả về danh sách rỗng
        }

        // Hàm lấy danh sách thương hiệu từ API
        private async Task<List<BrandModel>> GetBrandListAsync()
        {
            var response = await _httpClient.GetAsync(ApiEndpoints_Product.GET_HOME_BRANDS);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var brandList = JsonSerializer.Deserialize<List<BrandModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return brandList?.Where(b => b.IsAvailable).ToList() ?? new List<BrandModel>(); // Lọc theo IsAvailable và nếu null trả về danh sách rỗng
            }

            return new List<BrandModel>(); // Nếu API thất bại, trả về danh sách rỗng
        }

        // Hàm lấy danh sách danh mục từ API
        private async Task<List<CategoryModel>> GetCategoryListAsync()
        {
            var response = await _httpClient.GetAsync(ApiEndpoints_Product.GET_HOME_CATEGORIES);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var categoryList = JsonSerializer.Deserialize<List<CategoryModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return categoryList?.Where(c => c.IsAvailable).ToList() ?? new List<CategoryModel>(); // Lọc theo IsAvailable và nếu null trả về danh sách rỗng
            }

            return new List<CategoryModel>(); // Nếu API thất bại, trả về danh sách rỗng
        }
    }
}
