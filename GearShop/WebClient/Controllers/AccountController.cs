using Azure;
using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService accountService;
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;


        public AccountController(AccountService accountService, IHttpContextAccessor contx)
        {
            this.accountService = accountService;
            _contx = contx;
            client = new HttpClient();
        }
        // GET: Account/ChangePassword
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            string username = _contx.HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                var usernameCookie = _contx.HttpContext.Request.Cookies["username"];
                if (!string.IsNullOrEmpty(usernameCookie))
                {
                    username = usernameCookie;
                    _contx.HttpContext.Session.SetString("username", usernameCookie);
                }
            }
            if (!string.IsNullOrEmpty(username))
            {
                return View();
            }



            return Redirect("/Login");
        }



        [HttpPost("/Account/OnPostChangePasswordAsync")]
        public async Task<IActionResult> OnPostChangePasswordAsync(string oldpassword, string newpassword, string repassword)
        {
            string username = _contx.HttpContext.Session.GetString("username");

            try
            {
                if (repassword != newpassword)
                {
                    TempData["ErrorMessage"] = "Mật khẩu xác nhận không khớp.";
                    return RedirectToAction("ChangePassword");
                }

                ChangePasswordModel model = new ChangePasswordModel
                {
                    Username = username,
                    OldPassword = accountService.CalculateMD5Hash(oldpassword),
                    NewPassword = accountService.CalculateMD5Hash(newpassword)
                };

                var response = await client.PostAsJsonAsync(ApiEndpoints_Customer.CHANGE_PASSWORD, model);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginAccountModel>(jsonString);

                    if (customer == null)
                    {
                        TempData["ErrorMessage"] = "ChangePassword failed.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Password changed successfully!";
                    }

                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    TempData["ErrorMessage"] = "ChangePassword failed.";
                    return RedirectToAction("ChangePassword");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction("ChangePassword");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost("/Account/CheckEmailAsync")]
        public async Task<IActionResult> CheckEmailAsync(string email)
        {
            var encodedEmail = Uri.EscapeDataString(email);

            var url = string.Format(ApiEndpoints_Customer.CHECK_MAIL, encodedEmail);
            Console.WriteLine($"Constructed URL: {url}");
            var response = await client.GetAsync(url);
            if (response == null)
            {
                Console.WriteLine("Response is null");
                return Content("false");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }

        [HttpPost("/Account/SendOTP")]
        public IActionResult SendOTP(string email)
        {
            var otp = accountService.VerifyEmail(email);
            Console.WriteLine($"OTP generated: {otp}");
            return Json(new { otp = otp });
        }

        [HttpPost("/Account/ForgetPassword")]
        public IActionResult ForgetPassword(string password, string emailSend)
        {
            if (accountService.ForgetPassword(password, emailSend))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAddressOrder(DeliveryAddressModel addressModel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string userSession = _contx.HttpContext.Session.GetString("username");
            addressModel.Username = userSession;

            string jsonData = JsonSerializer.Serialize(addressModel, options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync(ApiEndpoints_Customer.ADD_ADDRESS, content);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddressOrder(DeliveryAddressModel addressModel)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            addressModel.Username = userSession;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string jsonData = JsonSerializer.Serialize(addressModel, options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync(ApiEndpoints_Customer.UPDATE_ADDRESS, content);
            return Ok();
        }

        [HttpGet("/Account/MyOrder")]
        public async Task<IActionResult> MyOrder(string username)
        {
            string userSession = _contx.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userSession))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                DataResult dataResult = new DataResult();

                HttpResponseMessage response = await client.GetAsync($"{ApiEndPoints_Order.GET_ORDER_BY_USERNAME}?username={userSession}");

                if (response.IsSuccessStatusCode)
                {
                    string stOrder = await response.Content.ReadAsStringAsync();

                    List<OrderDataModel> orderData = JsonSerializer.Deserialize<List<OrderDataModel>>(stOrder, options);
                    dataResult.Result = orderData;
                }
                return View(dataResult);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> OrderDetail(string id)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                DataResult dataResult = new DataResult();

                HttpResponseMessage responseOrder = await client.GetAsync($"{ApiEndPoints_Order.GET_ORDER_BY_ID}?Order_ID={id}");
                HttpResponseMessage responseDetails = await client.GetAsync($"{ApiEndPoints_Order.GET_ORDER_DETAILS}?Order_ID={id}");

                string stOrder = await responseOrder.Content.ReadAsStringAsync();
                string stOrderDetails = await responseDetails.Content.ReadAsStringAsync();

                var a = JsonSerializer.Deserialize<OrderModel>(stOrder, options);
                var orderThis = JsonSerializer.Deserialize<List<OrderDetailModel>>(stOrderDetails, options);
                var rs = new
                {
                    orderDetails = orderThis,
                    orderDick = a
                };
                return Json(rs);
            }
            catch
            {
                return RedirectToAction("/StatusCodeError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderId, int status)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                HttpResponseMessage responseOrder = await client.GetAsync($"{ApiEndPoints_Order.GET_ORDER_BY_ID}?Order_ID={orderId}");
                string stOrder = await responseOrder.Content.ReadAsStringAsync();

                // Get the order by orderId (this step depends on how you retrieve your orders)
                var order = JsonSerializer.Deserialize<OrderModel>(stOrder, options);
                order.Status = status;
                if (order == null)
                {
                    return NotFound();
                }
                var orderJson = JsonSerializer.Serialize(order, options);
                var contentOrder = new StringContent(orderJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(ApiEndPoints_Order.CHANGE_ORDER_STATUS, contentOrder);
                bool isStatusChanged = response.IsSuccessStatusCode;
                if (!isStatusChanged)
                {
                    return StatusCode(500, "Failed to cancel the order.");
                }

                return Json(new { redirectToUrl = Url.Action("MyOrder", "Account") });
            }
            catch (Exception ex)
            {

                return Json(new { redirectToUrl = Url.Action("MyOrder", "Account") });
            }
        }
    }
}
