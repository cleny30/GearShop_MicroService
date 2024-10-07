using BusinessObject.Core;
using BusinessObject.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;

namespace WebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Products/Details/{productId}
        [HttpGet("Products/Details/{productId}")]
        public async Task<IActionResult> Details(string productId)
        {
            HttpResponseMessage response = await _client.GetAsync($"{ApiEndpoints_Product.GET_PRODUCT_DETAILS}/{productId}");

            if (response.IsSuccessStatusCode)
            {
                string strProduct = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                ProductData product = JsonSerializer.Deserialize<ProductData>(strProduct, options);
                return View(product);
            }

            return NotFound();
        }
    }
}