using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = new AddressRepository();
        }


        // GET: api/Address
        [HttpGet("GetAddress")]
        public IActionResult GetAllAddress(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }

            var addresses = _addressRepository.GetAllAddress(username);

            if (addresses == null || !addresses.Any())
            {
                return NotFound("No addresses found for the specified username.");
            }

            return Ok(addresses);
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                var customer = await _customerRepository.ChangePassword(model);

                return Ok(new { message = "Password changed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
