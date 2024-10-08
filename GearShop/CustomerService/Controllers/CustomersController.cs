using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
      

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            
        }

        // GET: api/Customers/{username}
        [HttpGet("{username}")]
        public IActionResult GetCustomerByName(string username)
        {
            try
            {
                var customer = _customerRepository.GetCustomerByUserName(username);
                if (customer == null)
                {
                    return NotFound($"Customer with username '{username}' not found.");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // PUT: api/Customers
        [HttpPut("{username}")]
        public IActionResult UpdateCustomer(string username, CustomerModel customer)
        {
            try
            {
                var cus = _customerRepository.GetCustomerByUserName(username);
                if (cus == null) return NotFound();
                _customerRepository.UpdateCustomer(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetCustomerByUsername/{username}/{pass}")]
        public async Task<IActionResult> LoginAsync(string username, string pass)
        {
            var account = await _customerRepository.LoginCustomer(username, pass);

            if (account != null)
            {
                return Ok(account);
            }
            return NotFound();
        }
    }
}
