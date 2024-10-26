using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using DataAccess.DAO;
using BusinessObject.Model.Entity;

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

        [HttpGet("AddressByUsername")]
        public IActionResult GetAddressesByUsername(string username)
        {
            var addresses = AddressDAO.GetAddressByUsername(username);
            if (addresses == null || !addresses.Any())
            {
                return NotFound("No addresses found.");
            }
            return Ok(addresses);
        }

        // Thêm địa chỉ mới cho một người dùng
        [HttpPost("AddNewAddress/{username}")]
        public IActionResult AddNewAddress(string username, [FromBody] DeliveryAddressModel addressModel)
        {
            try
            {
                var success = AddressDAO.AddNewAddress(addressModel, username);
                if (success)
                {
                    return Ok("Thêm địa chỉ thành công.");
                }
                return BadRequest("Không thể thêm địa chỉ.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Cập nhật một địa chỉ
        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress([FromBody] DeliveryAddressModel addressModel)
        {
            try
            {
                var success = AddressDAO.UpdateAddress(addressModel);
                if (success)
                {
                    return Ok("Cập nhật địa chỉ thành công.");
                }
                return NotFound("Không tìm thấy địa chỉ cần cập nhật.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // Xóa một địa chỉ
        [HttpDelete("DeleteAddress/{username}/{id}")]
        public IActionResult DeleteAddress(string username, int id)
        {
            try
            {
                var success = AddressDAO.DeleteAddress(username, id);
                if (success)
                {
                    return Ok("Xóa địa chỉ thành công.");
                }
                return NotFound("Không tìm thấy địa chỉ cần xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find")]
        public IActionResult FindExistingAddress(string username, string phoneNumber, string fullname, string address, bool isDefault)
        {
            var existingAddress = AddressDAO.FindExistingAddressItem(username, phoneNumber, fullname, address, isDefault);
            if (existingAddress == null)
            {
                return NotFound("No matching address found.");
            }
            return Ok(existingAddress);
        }

        [HttpGet("GetAllAddressIsDefault")]
        public IActionResult GetAllAddressIsDefault(string username)
        {
            var addresses = AddressDAO.GetAllAddress(username);
            if (addresses == null || !addresses.Any())
            {
                return NotFound("No addresses found.");
            }
            return Ok(addresses);
        }
    }
}
