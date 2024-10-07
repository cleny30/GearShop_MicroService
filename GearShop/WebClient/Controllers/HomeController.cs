using BusinessObject;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;



namespace WebClient.Controllers
{
    public class HomeController : Controller
    {

        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HeaderService _headerService;
        private readonly IHomeRepository repo;

        public HomeController(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor contextAccessor,
            HeaderService headerService)
        {

            _client = httpClientFactory.CreateClient();
            _contextAccessor = contextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            _headerService = headerService;
        }

        public async Task<IActionResult> Index()
        {

            var sessionHeaderData = _contextAccessor.HttpContext.Session.GetString("HeaderData");
            var response = await _client.GetAsync(ApiEndpoints_Product.GET_HOME_PRODUCTS);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var jsonData = await response.Content.ReadAsStringAsync();
                HomeModel homeModel = System.Text.Json.JsonSerializer.Deserialize<HomeModel>(jsonData, options);

                // Fetch brand data
                var brandResponse = await _client.GetAsync(ApiEndpoints_Product.GET_HOME_BRANDS);
                if (brandResponse.IsSuccessStatusCode)
                {
                    var brandJsonData = await brandResponse.Content.ReadAsStringAsync();
                    List<Brand> brands = System.Text.Json.JsonSerializer.Deserialize<List<Brand>>(brandJsonData, options);

                    // Pass the brand list to the HomeModel
                    homeModel.Brands = brands;
                }

                // Fetch cate data
                var cateResponse = await _client.GetAsync(ApiEndpoints_Product.GET_HOME_CATEGORIES);
                if (cateResponse.IsSuccessStatusCode)
                {
                    var cateJsonData = await cateResponse.Content.ReadAsStringAsync();
                    List<Category> categories = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(cateJsonData, options);

                    // Pass the brand list to the HomeModel
                    homeModel.Categories = categories;
                }

                // Pass the HomeModel to the view
                return View(homeModel);

            }

            return NoContent();

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
