using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;

        public OrderController(IHttpContextAccessor contx)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _contx = contx;
        }

        [HttpGet("/Order")]
        public async Task<IActionResult> Index()
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            var productuserSession = _contx.HttpContext.Session.GetString("proId");
            if (!string.IsNullOrEmpty(userSession))
            {
                if (!string.IsNullOrEmpty(productuserSession))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    string urlCartData = string.Format(ApiEndPoints_Cart.GET_CHECKED_PRODUCTS, userSession, productuserSession);
                    string urlAddress = string.Format(ApiEndpoints_Customer.GET_ALL_ADDRESS, userSession);

                    HttpResponseMessage response = await client.GetAsync(urlCartData);
                    HttpResponseMessage address = await client.GetAsync(urlAddress);

                    string strCartData = await response.Content.ReadAsStringAsync();
                    string strAddress = await address.Content.ReadAsStringAsync();

                    var list = JsonSerializer.Deserialize<List<UserCartData>>(strCartData, options);

                    var addresses = JsonSerializer.Deserialize<List<DeliveryAddressModel>>(strAddress, options);


                    if (addresses.Count() == 0)
                    {
                        return RedirectToAction("MyAddress", "Account");
                    }

                    DataResult data = new DataResult();

                    data.Result = new
                    {
                        list = list,
                        addresses = addresses
                    };

                    return View(data);
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult StoreCheckedProduct(List<string> proIds)
        {
            _contx.HttpContext.Session.SetString("proId", string.Join(',', proIds));
            return Content("OK");
        }
    }
}
