using BusinessObject.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
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

        [HttpPost]
        public bool CreateAddress([FromBody]DeliveryAddressModel deliveryAddressModel)
        {
            string username = deliveryAddressModel.Username;
            return _addressRepository.AddNewAddress(deliveryAddressModel, username);

        }

        [HttpPut]
        public bool UpdateAddress([FromBody] DeliveryAddressModel addressModel)
        {
            return _addressRepository.UpdateAddress(addressModel);
        }
    }
}
