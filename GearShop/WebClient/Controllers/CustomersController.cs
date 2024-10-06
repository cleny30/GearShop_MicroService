using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Identity.Client;
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
        [HttpGet]
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

        [HttpPost("/Customer/UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(CustomerModel accountModel, string username)
        {
          
             username = httpContextAccessor.HttpContext.Session.GetString("username");

           
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            
            var jsonContent = JsonContent.Create(accountModel);

           
            HttpResponseMessage response = await client.PutAsync($"{ApiEndpoints_Customer.UPDATE_CUSTOMER_BY_USERNAME}/{username}", jsonContent);
           
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {responseContent}");
            if (response.IsSuccessStatusCode)
            {
               
                TempData["SuccessMessage"] = "Update sucessfull!";

                // Redirect to the Details page
                return RedirectToAction("Details");
            }
            else
            {
                // If the API returns an error, retrieve error content and display it in the view
                string errorContent = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"Error updating account data: {errorContent}";
                return View(accountModel); // Return the view with the current model to show validation errors
            }
        }



    }
}
