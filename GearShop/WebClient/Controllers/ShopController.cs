using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class ShopController : Controller
    {
        private readonly HttpClient client = null;
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string sortFilter = Request.Query["sort"].ToString();
            string orderFilter = Request.Query["order"].ToString();
            string category = Request.Query["category"].ToString();
            string brand = Request.Query["brand"].ToString();
            int currentPage = Request.Query["page"].ToString() != "" ? Convert.ToInt32(Request.Query["page"]) : 1;

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

            ShopModel data = _shopService.GetData(productList, brandList, categoryList, sortFilter, orderFilter, category, brand, currentPage);
            return View(data);
        }
    }
}
