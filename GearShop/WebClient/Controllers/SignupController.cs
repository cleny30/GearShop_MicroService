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

        [HttpPost("/Account/Register")]
        public async Task<IActionResult> Register(RegistModel registModel)
        {
            try
            {
                if (await accountService.RegisterAsync(registModel))
                {
                    TempData["SuccessMessage"] = "Registration successful! Please check your email for the OTP.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "Registration failed. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                return View("Register");
            }
        }

    }
}
