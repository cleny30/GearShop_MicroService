using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class SearchController : Controller
    {
        private readonly HttpClient client = null;
        private readonly ShopService _shopService;

        public SearchController(ShopService shopService)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _shopService = shopService;
        }
        [HttpGet]
        public async Task<IActionResult> SearchProductByName(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return Json(new { result = new List<ProductData>() });
            }

            // Make a request to the API to get the products based on the search pattern
            var response = await client.GetAsync($"{ApiEndpoints.GET_PRODUCT_BY_NAME}/{pattern}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var jsonData = await response.Content.ReadAsStringAsync();
                List<ProductData> productList = JsonSerializer.Deserialize<List<ProductData>>(jsonData, options);

                return Json(new { result = productList });
            }

            return Json(new { result = new List<ProductData>() });
        }

    }
}
