using BusinessObject.Core;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class AddressController : Controller
    {
        private readonly CustomerContext _context;
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AddressController(IHttpContextAccessor http)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            httpContextAccessor = http;
        }


        public IActionResult Index()
        {
            
                return View("Error");
            
        }
    }
}
