using BusinessObject.DTOS;
using CartService.SubService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        ICartRepository cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        [HttpPost("AddOrUpdateCart")]
        public ActionResult<bool> AddOrUpdateCart([FromBody] CartRequest request)
        {
            Service service = new Service(cartRepository);

            return service.AddOrUpdateCart(request.UserCartData, request.Amount);
        }

        [HttpGet]
        public ActionResult<List<CartModel>> GetCartByUsername(string username)
        {
            return cartRepository.GetCartsByUsername(username);
        }
    }
}
