
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Http.Headers;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;



namespace WebClient.Controllers
{
    public class HomeController : Controller
    {

        private readonly HttpClient client = null;
        private readonly ShopService _shopService;

        public HomeController(ShopService shopService)
        {

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {

            HttpResponseMessage _productcResponse = await client.GetAsync(ApiEndpoints_Product.GET_ALL_PRODUCTS);
            HttpResponseMessage _brandResponse = await client.GetAsync(ApiEndpoints_Product.GET_ALL_BRANDS);
            HttpResponseMessage _categoryResponse = await client.GetAsync(ApiEndpoints_Product.GET_ALL_CATEGORIES);

            string strProduct = await _productcResponse.Content.ReadAsStringAsync();
            string strBrand = await _brandResponse.Content.ReadAsStringAsync();
            string strCategory = await _categoryResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ProductData> productList = JsonSerializer.Deserialize<List<ProductData>>(strProduct, options);
            List<Brand> brandList = JsonSerializer.Deserialize<List<Brand>>(strBrand, options);
            List<Category> categoryList = JsonSerializer.Deserialize<List<Category>>(strCategory, options);

            List<CategoryModel> categoryModels = _shopService.GetCategoryList(categoryList).Where(c => c.IsAvailable == true).ToList();
            List<BrandModel> brandModels = _shopService.GetBrandList(brandList).Where(b => b.IsAvailable == true).ToList();
            ShopModel data = new ShopModel()
            {
                products = productList,
                brandModels = brandModels,
                categoryModels = categoryModels,
            };
            return View(data);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
