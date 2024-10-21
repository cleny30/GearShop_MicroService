using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDAO cartDAO = new CartDAO();
        public bool AddCart(CartModel _cart)=>cartDAO.Add(_cart);

        public bool DeleteCartById(string proId, string username)=> cartDAO.DeleteCartById(proId, username);

        public List<CartModel> GetCarts()
        {
            throw new NotImplementedException();
        }

        public List<CartModel> GetCartsByUsername(string username)=>cartDAO.GetCartsByUsername(username);

        public bool UpdateCartData(CartModel _cart)=> cartDAO.Update(_cart);
    }
}
