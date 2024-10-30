using Azure;
using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Identity.Client;
using System.Net;
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
            if (_customerResponse.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.ErrorMessage = "Không tìm thấy địa chỉ.";
                return View("MyAddress"); // Chuyển đến form thêm nếu không có địa chỉ
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DeliveryAddressModel> addressList = JsonSerializer.Deserialize<List<DeliveryAddressModel>>(strcustomer, options);

          

            return View(addressList); // Hiển thị danh sách địa chỉ nếu đã có
        }


        [HttpPost("/Account/AddAddress")]
        public async Task<IActionResult> AddAddress(DeliveryAddressModel newAddress)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu không có username trong session
            }

            // Gán tên người dùng cho địa chỉ mới
            newAddress.Username = username;

            // Chuẩn bị dữ liệu JSON và gọi API để thêm địa chỉ
            var jsonData = JsonSerializer.Serialize(newAddress);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ApiEndpoints_Customer.ADD_ADDRESS, content);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.ErrorMessage = "Không tìm thấy endpoint để thêm địa chỉ.";
                return View("Error"); // Hiển thị trang lỗi nếu không tìm thấy endpoint
            }
            else if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAddress"); // Chuyển hướng đến trang danh sách địa chỉ nếu thêm thành công
            }
            return RedirectToAction("MyAddress");
        }



        // PUT: Customers/UpdateAddress
        [HttpPost("/Account/UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(DeliveryAddressModel updatedAddress)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");
            updatedAddress.Username = username;

            var jsonData = JsonSerializer.Serialize(updatedAddress);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{ApiEndpoints_Customer.UPDATE_ADDRESS}/{updatedAddress.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAddress");
            }
            else
            {
                ViewBag.ErrorMessage = "Lỗi khi cập nhật địa chỉ.";
                return View("Error");
            }
        }

        [HttpPost("/Account/DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            string username = httpContextAccessor.HttpContext.Session.GetString("username");

            // Ensure the username is set before proceeding
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.ErrorMessage = "User not logged in.";
                return View("Error");
            }

            HttpResponseMessage response = await client.DeleteAsync($"{ApiEndpoints_Customer.DELETE_ADDRESS}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAddress"); // Redirect to MyAddress page on success
            }
            else
            {
                ViewBag.ErrorMessage = "Lỗi khi xoá địa chỉ.";
                return View("Error"); // Display error view on failure
            }
        }
    }
}