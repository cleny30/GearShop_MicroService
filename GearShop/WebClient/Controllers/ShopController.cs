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
        private readonly ProductService _proService;

        public ShopController(ShopService shopService, ProductService proService)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _shopService = shopService;
            _proService = proService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string sortFilter = Request.Query["sort"].ToString();
            string orderFilter = Request.Query["order"].ToString();
            string category = Request.Query["category"].ToString();
            string brand = Request.Query["brand"].ToString();
            int currentPage = Request.Query["page"].ToString() != "" ? Convert.ToInt32(Request.Query["page"]) : 1;

            HttpResponseMessage _productcResponse = await client.GetAsync(ApiEndpoints.GET_ALL_PRODUCTS);
            HttpResponseMessage _brandResponse = await client.GetAsync(ApiEndpoints.GET_ALL_BRANDS);
            HttpResponseMessage _categoryResponse = await client.GetAsync(ApiEndpoints.GET_ALL_CATEGORIES);
            HttpResponseMessage _proImgResponse = await client.GetAsync(ApiEndpoints.GET_ALL_PRODUCT_IMAGES);
            HttpResponseMessage _proAttResponse = await client.GetAsync(ApiEndpoints.GET_ALL_PRODUCT_ATTRIBUTES);

            string strProduct = await _productcResponse.Content.ReadAsStringAsync();
            string strBrand = await _brandResponse.Content.ReadAsStringAsync();
            string strCategory = await _categoryResponse.Content.ReadAsStringAsync();
            string strProImg = await _proImgResponse.Content.ReadAsStringAsync();
            string strProAtt = await _proAttResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> productList = JsonSerializer.Deserialize<List<Product>>(strProduct, options);
            List<ProductImage> proImgList = JsonSerializer.Deserialize<List<ProductImage>>(strProImg, options);
            List<ProductAttribute> ProAttList = JsonSerializer.Deserialize<List<ProductAttribute>>(strProAtt, options);
            List<Brand> brandList = JsonSerializer.Deserialize<List<Brand>>(strBrand, options);
            List<Category> categoryList = JsonSerializer.Deserialize<List<Category>>(strCategory, options);

            List<ProductData> productDatas = _proService.GetProducts(productList, proImgList, ProAttList);
            ShopModel data = _shopService.GetData(productDatas, brandList, categoryList, sortFilter, orderFilter, category, brand, currentPage);
            return View(data);
        }
    }
}
