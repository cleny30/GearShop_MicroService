using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebClient.APIEndPoint;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountService accountService;
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;

        public LoginController(AccountService accountService, IHttpContextAccessor contx)
        {
            this.accountService = accountService;
            _contx = contx;
            client = new HttpClient();
        }
        [HttpGet("/Login")]
        public IActionResult Index()
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
                return Redirect("/Home");
            }
            return View();
        }
        [HttpPost("/Login/OnPostLoginAsync")]
        public async Task<IActionResult> OnPostLoginAsync(string username, string password, bool isRemember)
        {
            DataResult data = new DataResult();

            try
            {
                LoginAccountModel model = new LoginAccountModel
                {
                    Username = username,
                    Password = password
                };
                var passClient = accountService.CalculateMD5Hash(model.Password);

                var url = string.Format(ApiEndpoints_Customer.GET_CUSTOMER_BY_USERNAME_LOGIN, username,passClient);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<LoginAccountModel>(jsonString);

                    if (customer == null)
                    {                     
                        data.Message = "Login fail";
                        data.IsSuccess = false;
                        return Content(data.IsSuccess.ToString());
                    }
                    else
                    {
                            data.Message = "Login success";
                            data.Result = model;

                            if (isRemember)
                            {
                                HttpContext.Session.SetString("username", username);
                                HttpContext.Response.Cookies.Append("username", username, new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    Expires = DateTime.Now.AddDays(3),
                                });
                            }
                            return Redirect("/Home");     
                    }                 
                }
                else
                {                 
                    data.Message = "Login fail";
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
