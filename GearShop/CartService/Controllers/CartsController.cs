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

        [HttpPost("UpdateCart")]
        public ActionResult<bool> UpdateCart([FromBody] CartModel data)
        {
            return cartRepository.UpdateCartData(data);
        }

        [HttpDelete("Delete/{proId}/{username}")]
        public ActionResult<bool> DeleteCart(string proId, string username)
        {
            return cartRepository.DeleteCartById(proId, username);
        }
    }
}
