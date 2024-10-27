using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class SignupController : Controller
    {
        private readonly AccountService accountService;
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client = null;


        public SignupController(AccountService accountService, IHttpContextAccessor contx)
        {
            this.accountService = accountService;
            _contx = contx;
            client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("/Signup/OnPostRegister")]
        public IActionResult OnPostRegister(RegistModel registModel)
        {
            if (accountService.Register(registModel))
            {
                var otp = accountService.VerifyEmail(registModel.Email);
                Console.WriteLine($"OTP generated: {otp}");
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("VerifyEmail", "Account");
            }
            else
            {
                TempData["ErrorMessage"] = "Registration failed. Please try again.";
                return View("Register");
            }
        }
    }
}
