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
        [HttpPost("RegisterUsername")]
        public async Task<IActionResult> RegisterAsync(RegisterModel register)
        {
            if (register == null)
            {
                return BadRequest(new { message = "Invalid registration data." });
            }

            // Log các giá trị của register
            Console.WriteLine($"Username: {register.Username}, Email: {register.Email}");

            var account = await _customerRepository.Register(register);
            if (account)
            {
                return Ok(new { message = "Account registered successfully." });
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

        [HttpGet("CheckMail/{mail}")]
        public async Task<IActionResult> CheckMail(string mail)
        {
            bool result = await _customerRepository.CheckMail(mail);


            if (result)
            {
                return Ok(new { message = "Email exists." });
            }
            else
            {
                return BadRequest(new { message = "Email not found." });
            }
        }   

        [HttpGet("CheckMailRegister/{mail}")]
        public async Task<IActionResult> CheckMailRegister(string mail)
        {
            bool result = await _customerRepository.CheckMail(mail);


            if (!result)
            {
                return Ok(new { message = "Email not found." });
            }
            else
            {
                return BadRequest(new { message = "Email exists." });
            }
        }  

        [HttpGet("CheckUsername/{username}")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            bool result = await _customerRepository.CheckUsername(username);


            if (!result)
            {
                return Ok(new { message = "User not found." });
            }
            else
            {
                return BadRequest(new { message = "User exsit" });
            }
        }

        [HttpGet("ForgetPassword/{mail}/{pass}")]
        public IActionResult ForgetPassword(string mail, string pass)
        {
            try
            {
                _customerRepository.ForgetPassword(mail,pass);
                return Ok(new { message = "Password reset successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
