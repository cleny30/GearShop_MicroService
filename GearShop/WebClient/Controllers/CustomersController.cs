using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebClient.APIEndPoint;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebClient.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerContext _context;
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CustomersController(IHttpContextAccessor http)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            httpContextAccessor = http;
        }


        // GET: Customers/Details/{name}
        [HttpGet("Account/MyAccount")]
        public async Task<IActionResult> Details()
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync($"{ApiEndpoints_Customer.GET_CUSTOMER_BY_USERNAME}/{username}");

            if (response.IsSuccessStatusCode)
            {
                string strCustomer = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                CustomerModel customer = JsonSerializer.Deserialize<CustomerModel>(strCustomer, options);
                return View(customer);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"Error fetching customer data: {errorContent}";
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CustomerModel accountModel)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            accountModel.Username = username;
            string jsonData = JsonSerializer.Serialize(accountModel, options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            string url = string.Format(ApiEndpoints_Customer.UPDATE_CUSTOMER_BY_USERNAME, username);
            HttpResponseMessage response = await client.PutAsync(url, content);
            return Ok(true);
        }

        [HttpGet("/Account/MyAddress")]
        public async Task<IActionResult> MyAddress()
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            string url = string.Format(ApiEndpoints_Customer.GET_ALL_ADDRESS, username);
            HttpResponseMessage _customerResponse = await client.GetAsync(url);
            string strcustomer = await _customerResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DeliveryAddressModel> addressList = JsonSerializer.Deserialize<List<DeliveryAddressModel>>(strcustomer, options);
            return View(addressList);
        }


        [HttpPost("/Account/AddAddress")]
        public async Task<IActionResult> AddNewAddress(DeliveryAddressModel addressModel)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Username không tồn tại.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonData = JsonSerializer.Serialize(addressModel, options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST đến API
            string url = string.Format(ApiEndpoints_Customer.ADD_NEW_ADDRESS, username);
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAddress");
            }

            string errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = $"Có lỗi khi thêm địa chỉ: {errorContent}";
            return View("Error");
        }


        [HttpPost("/Account/UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(int id, DeliveryAddressModel addressModel)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Username không tồn tại.");
            }

            if (id != addressModel.Id)
            {
                return BadRequest("Dữ liệu địa chỉ không khớp.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonData = JsonSerializer.Serialize(addressModel, options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Gửi yêu cầu PUT đến API
            string url = string.Format(ApiEndpoints_Customer.UPDATE_ADDRESS, id);
            HttpResponseMessage response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAddress");
            }

            string errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = $"Có lỗi khi cập nhật địa chỉ: {errorContent}";
            return View("Error");
        }



    }
}
