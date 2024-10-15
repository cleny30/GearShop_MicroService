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
        private readonly IHttpContextAccessor _contx;
        public HomeController(ShopService shopService, IHttpContextAccessor contx)
        {

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _shopService = shopService;
            _contx = contx;
        }

        public async Task<IActionResult> Index()
        {
            string username = null;
            int count = 0;
            if (_contx.HttpContext.Session.GetString("username") != null)
            {
                username = _contx.HttpContext.Session.GetString("username");
            }
            else
            {
                var usernameCookie = _contx.HttpContext.Request.Cookies["username"];
                if (!string.IsNullOrEmpty(usernameCookie))
                {
                    username = usernameCookie;
                    _contx.HttpContext.Session.SetString("username", usernameCookie);
                }
            }
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
            if (!string.IsNullOrEmpty(username))
            {
                var _cartResponse = await client.GetAsync($"{ApiEndPoints_Cart.GET_CART_BY_USERNAME}?username={username}");
                string strCart = await _cartResponse.Content.ReadAsStringAsync();
                List<CartModel> cartList = JsonSerializer.Deserialize<List<CartModel>>(strCart, options);
                count = cartList.Count();
            }
                _contx.HttpContext.Session.SetString("cartQuantity", Newtonsoft.Json.JsonConvert.SerializeObject(count));
            
            ViewBag.Username = username;
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
