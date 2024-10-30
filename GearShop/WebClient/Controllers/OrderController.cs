using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;
        private readonly OrderService orderService;
        public OrderController(IHttpContextAccessor contx, OrderService orderService)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _contx = contx;
            this.orderService = orderService;
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
                    var addresses = new List<DeliveryAddressModel>();

                    if (!string.IsNullOrEmpty(strAddress)) {
                        addresses = JsonSerializer.Deserialize<List<DeliveryAddressModel>>(strAddress, options);
                    }
                    var list = JsonSerializer.Deserialize<List<UserCartData>>(strCartData, options);

                   
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

        [HttpPost]
        public async Task<DataResult> CheckOut(OrderModel order)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            var productChecked = _contx.HttpContext.Session.GetString("proId");

            OrderModel orderModel = new OrderModel
            {
                Address = order.Address,
                OrderDes = order.OrderDes == null ? order.OrderDes : "None",
                Fullname = order.Fullname,
                Phone = order.Phone,
                proId = productChecked,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Status = 1,
                TotalPrice = order.TotalPrice,
                Username = userSession,
            };
            DataResult result = new DataResult();

            result.IsSuccess = await orderService.Checkout(orderModel);
            return result;
        }

        [HttpGet("/Order/PostCheckout")]
        public IActionResult PostCheckout()
        {
            _contx.HttpContext.Session.Remove("proId");
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
