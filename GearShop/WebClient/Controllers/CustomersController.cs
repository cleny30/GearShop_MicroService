using BusinessObject.Core;
using BusinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net.Http;
using WebClient.APIEndPoint;

namespace WebClient.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerContext _context;
        private readonly HttpClient client;

        // GET: Customers/Details/{name}
        public async Task<IActionResult> Details(string name)
        {
            var response = await client.GetAsync($"{APIEndpoint_Customer.GET_CUSTOMER_BY_USERNAME}/{name}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadFromJsonAsync<CustomerModel>();
                return View(customer);
            }
            else
            {
                ViewBag.ErrorMessage = $"Customer with username '{name}' not found.";
                return View("Error");
            }
        }

        // POST: Customers/Update
        [HttpPost]
        public async Task<IActionResult> Update(CustomerModel customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Username))
            {
                ModelState.AddModelError(string.Empty, "Invalid customer data.");
                return View(customer);
            }

            var response = await client.PutAsJsonAsync($"{APIEndpoint_Customer.UPDATE_CUSTOMER_BY_USERNAME}", customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update customer.");
                return View(customer);
            }
        }

    }
}
