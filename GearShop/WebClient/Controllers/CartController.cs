using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;
        public CartController(IHttpContextAccessor contx)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _contx = contx;
        }

        [HttpGet("/Cart")]
        public async Task<IActionResult> Index()
        {
            DataResult dataResult = new DataResult();
            string userSession = _contx.HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(userSession))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var _productcResponse = await client.GetAsync(ApiEndpoints_Product.GET_ALL_PRODUCTS);
                var _cartResponse = await client.GetAsync($"{ApiEndPoints_Cart.GET_CART_BY_USERNAME}?username={userSession}");

                string strProduct = await _productcResponse.Content.ReadAsStringAsync();
                string strCart = await _cartResponse.Content.ReadAsStringAsync();

                List<ProductData> productList = JsonSerializer.Deserialize<List<ProductData>>(strProduct, options);
                List<CartModel> cartList = JsonSerializer.Deserialize<List<CartModel>>(strCart, options);

                List<UserCartData> list = cartList.Select(item => new UserCartData
                {
                    CartModel = item,
                    Product = productList.FirstOrDefault(p => p.ProId == item.ProId)
                }).ToList();
                return View(list);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [Route("Cart/AddProductToCart")]
        public async Task<IActionResult> AddProductToCart(string data, int amount)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(userSession))
            {
                ProductData productData = System.Text.Json.JsonSerializer.Deserialize<ProductData>(data);


                var cartRequest = new CartRequest
                {
                    UserCartData = new UserCartData
                    {
                        CartModel = new CartModel
                        {
                            Username = userSession,
                            ProId = productData.ProId,
                            ProName = productData.ProName,
                        },
                        Product = productData
                    },
                    Amount = amount
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string jsonData = JsonSerializer.Serialize(cartRequest, options);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var str = ApiEndPoints_Cart.ADD_OR_UPDATE;
                var _response = await client.PostAsync(ApiEndPoints_Cart.ADD_OR_UPDATE, content);

                if (_response.IsSuccessStatusCode)
                {
                    return Ok(true);
                }
                else
                {
                    // Handle error response
                    return StatusCode((int)_response.StatusCode, "Failed to add product to cart.");
                }
            }
            else
            {
                return Unauthorized("User session is not available.");
            }
        }
    }
}
