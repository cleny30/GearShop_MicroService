using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Net.Http.Json;
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



            return View();
        }



        [HttpPost("/Account/OnPostChangePasswordAsync")]
        public async Task<IActionResult> OnPostChangePasswordAsync(string oldpassword, string newpassword, string repassword)
        {
            DataResult data = new DataResult();
            string username = _contx.HttpContext.Session.GetString("username");
            try
            {
                if (repassword != newpassword)
                {
                    data.Message = "Mật khẩu xác nhận không khớp.";
                    data.IsSuccess = false;
                    return Content(data.IsSuccess.ToString());
                }

                ChangePasswordModel model = new ChangePasswordModel
                {
                    Username = username,
                    OldPassword = oldpassword,
                    NewPassword = newpassword
                };


                var response = await client.PostAsJsonAsync(ApiEndpoints_Customer.CHANGE_PASSWORD, model);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<LoginAccountModel>(jsonString);

                    if (customer == null)
                    {
                        data.Message = "ChangePassword fail";
                        data.IsSuccess = false;
                    }
                    else
                    {
                        data.Message = "ChangePassword success";
                        data.Result = model;
                    }

                    return Redirect("/Account/ChangePassword");
                }
                else
                {
                    data.Message = "ChangePassword fail";
                    data.IsSuccess = false;
                    return Content(data.IsSuccess.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }

        }
    }
}
