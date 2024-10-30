using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface ICartRepository
    {
        public List<CartModel> GetCarts();
        public List<CartModel> GetCartsByUsername(string username);
        public bool AddCart(CartModel _cart);
        public bool UpdateCartData(CartModel _cart);
        public bool DeleteCartById(string proId, string username);
        public List<UserCartData> GetUserCartDatas(string username, List<string> proIds);
    }
}
