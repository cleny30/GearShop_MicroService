using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Text;
using WebClient.APIEndPoint;
using WebClient.Models;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AccountService accountService;
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;

        public RegisterController(AccountService accountService, IHttpContextAccessor contx)
        {
            this.accountService = accountService;
            _contx = contx;
            client = new HttpClient();
        }

        [HttpGet("/Signup")]
        public IActionResult Register()
        {
            return View();
        } 
        [HttpGet("/RegisterOTP")]
        public IActionResult RegisterOTP()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckUserExist(string username)
        {
            DataResult data = new DataResult();
            var url = string.Format(ApiEndpoints_Customer.GET_CUSTOMER_BY_USERNAME, username);
            var response = await client.GetAsync(url);
         
            if (response.IsSuccessStatusCode)
            {
                data.Message = "Username exist";
                data.IsSuccess = false;
            }
            else
            {
                data.Message = "Username not exist";
            }
            return Content(data.IsSuccess.ToString());
        }
        [HttpPost]
        public async Task<IActionResult> CheckMailExist(string mail)
        {
            DataResult data = new DataResult();
            var url = string.Format(ApiEndpoints_Customer.CHECK_MAIL, mail);
            var response = await client.GetAsync(url);
         
            if (response.IsSuccessStatusCode)
            {
                data.Message = "Mail exist";
                data.IsSuccess = false;
            }
            else
            {
                data.Message = "Mail not exist";
            }
            return Content(data.IsSuccess.ToString());
        }
        [HttpPost]
        public async Task<IActionResult> OnPostRegisterAsync(string username, string fullname, string phone, string email, string password, string rePassword)
        {
            DataResult data = new DataResult();
            var passClient = accountService.CalculateMD5Hash(password);
            // Tạo model từ các tham số
            RegisterModel register = new RegisterModel
            {
                Username = username,
                Fullname = fullname,
                Phone = phone,
                Email = email,
                Password = passClient,
            };

            // Kiểm tra URL
            var url = ApiEndpoints_Customer.REGISTER_CUSTOMER;
            Console.WriteLine($"Register URL: {url}");  // Log URL

            if (string.IsNullOrEmpty(url))
            {
                data.Message = "API URL is null";
                data.IsSuccess = false;
                return Content(data.Message);
            }

            // Serialize model sang JSON và kiểm tra
            var jsonContent = JsonConvert.SerializeObject(register);
            Console.WriteLine($"Serialized JSON: {jsonContent}");  // Log JSON

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                // Gửi POST request
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    data.Message = "Register success";
                    data.IsSuccess = true;
                }
                else
                {
                    data.Message = "Register failed";
                    data.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                data.Message = $"Error: {ex.Message}";
                data.IsSuccess = false;
            }

            return Content(data.IsSuccess.ToString());
        }


    }
}
