using BusinessObject.DTOS;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Customers/{name}
        [HttpGet("{name}")]
        public IActionResult GetCustomerByName(string name)
        {
            try
            {
                var customer = _customerRepository.GetCustomerByName(name);
                if (customer == null)
                {
                    return NotFound($"Customer with username '{name}' not found.");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // PUT: api/Customers
        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                if (customer == null || string.IsNullOrEmpty(customer.Username))
                {
                    return BadRequest("Invalid customer data.");
                }

                _customerRepository.UpdateCustomer(customer);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
