using BusinessObject.DTOS;
using DataAccess.DAO;
using ISUZU_NEXT.Server.Core.Extentions;
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

        public List<UserCartData> GetUserCartDatas(string username, List<string> proIds)
        {
            List<UserCartData> cartItems = new List<UserCartData>();
            foreach (var i in proIds)
            {
                cartItems.Add(GetCartDataByUserName(username).FirstOrDefault(p => p.Product.ProId == i));
            }
            return cartItems;
        }

        public bool UpdateCartData(CartModel _cart)=> cartDAO.Update(_cart);

        private List<UserCartData> GetCartDataByUserName(string username)
        {
            var products = ProductDAO.GetProducts();
            var carts = GetCartsByUsername(username).ToList();
            List<UserCartData> list = new List<UserCartData>();
            foreach (CartModel item in carts)
            {
                ProductData pData = new ProductData();
                pData.CopyProperties(products.FirstOrDefault(p => p.ProId == item.ProId));
                UserCartData cartData = new UserCartData
                {
                    CartModel = item,
                    Product = pData
                };
                list.Add(cartData);
            }
            return list;
        }
    }
}
