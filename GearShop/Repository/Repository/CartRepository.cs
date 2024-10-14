using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDAO cartDAO = new CartDAO();
        public bool AddCart(CartModel _cart)=>cartDAO.AddOrUpdateCart(_cart);

        public void DeleteCartById(string proId, string username)
        {
            throw new NotImplementedException();
        }

        public List<CartModel> GetCarts()
        {
            throw new NotImplementedException();
        }

        public List<CartModel> GetCartsByUsername(string username)=>cartDAO.GetCartsByUsername(username);

        public bool UpdateCartData(CartModel _cart)
        {
            throw new NotImplementedException();
        }
    }
}
