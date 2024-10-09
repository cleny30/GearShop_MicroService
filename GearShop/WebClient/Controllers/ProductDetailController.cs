using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;

namespace WebClient.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly HttpClient client = null;
        public ProductDetailController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet("/Details/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string url = string.Format(ApiEndpoints_Product.GET_PRODUCT_DETAIL, id);
            HttpResponseMessage _productcResponse = await client.GetAsync(url);
            string strProduct = await _productcResponse.Content.ReadAsStringAsync();

            ProductDetailModel data = JsonSerializer.Deserialize<ProductDetailModel>(strProduct, options);

            return View(data);
        }
    }
}
