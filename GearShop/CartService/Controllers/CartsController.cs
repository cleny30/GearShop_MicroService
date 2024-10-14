using BusinessObject.DTOS;
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

        [HttpPost("AddToCart")]
        public ActionResult<bool> AddToCart([FromBody] CartModel cart)
        {
            return cartRepository.AddCart(cart);
        }

        [HttpGet]
        public ActionResult<List<CartModel>> GetCartByUsername(string username)
        {
            return cartRepository.GetCartsByUsername(username);
        }
    }
}
