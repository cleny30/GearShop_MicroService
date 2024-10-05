using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string sortFilter = Request.Query["sort"].ToString();
            string orderFilter = Request.Query["order"].ToString();
            string category = Request.Query["category"].ToString();
            string brand = Request.Query["brand"].ToString();
            int currentPage = Request.Query["page"].ToString() != "" ? Convert.ToInt32(Request.Query["page"]) : 1; ;

            return View();
        }
    }
}
