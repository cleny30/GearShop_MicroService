using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CartDAO
    {
        public List<CartModel> GetCarts()
        {
            List<Cart> cart;
            try
            {
                var dbContext = new CartContext();
                cart = dbContext.Carts.ToList();

                List<CartModel> CartModels = new List<CartModel>();

                foreach (var item in cart)
                {
                    CartModel CartModel = new CartModel();
                    CartModel.CopyProperties(item);
                    CartModels.Add(CartModel);
                }
                return CartModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CartModel> GetCartsByUsername(string username)
        {
            List<CartItem> cart;
            try
            {
                using (var dbContext = new CartContext())
                {
                    cart = dbContext.CartItems.ToList();

                    List<CartModel> CartModels = new List<CartModel>();

                    foreach (var item in cart)
                    {
                        CartModel CartModel = new CartModel();
                        CartModel.CopyProperties(item);
                        CartModel.ProPrice = item.Price;
                        CartModel.Username = username;
                        CartModels.Add(CartModel);
                    }
                    return CartModels.Where(u => u.Username == username).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Add(CartModel _cart)
        {
            if (_cart == null)
            {
                throw new ArgumentNullException(nameof(_cart), "CartModel cannot be null");
            }
            try
            {
                using (var dbContext = new CartContext())
                {
                    var existCart = dbContext.Carts.FirstOrDefault(c => c.Username == _cart.Username);
                    int result = 0;
                    if (existCart == null)
                    {
                        Cart cart = new Cart();
                        //TODO ADD NEW CART
                        cart.Username = _cart.Username;
                        cart.ProPrice = _cart.ProPrice * _cart.Quantity;
                        dbContext.Carts.Add(cart);
                        dbContext.SaveChanges();
                        CartItem item = new CartItem();
                        item.CopyProperties(_cart);
                        item.CartId = cart.CartId;
                        item.Price = _cart.ProPrice;
                        dbContext.CartItems.Add(item);
                        result = dbContext.SaveChanges();
                    }
                    else
                    {
                        existCart.ProPrice += _cart.ProPrice * _cart.Quantity;
                        dbContext.Entry<Cart>(existCart).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        CartItem item = new CartItem();
                        item.CopyProperties(_cart);
                        item.CartId = existCart.CartId;
                        item.Price = _cart.ProPrice;
                        dbContext.CartItems.Add(item);
                        result = dbContext.SaveChanges();
                        
                    }
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

        }
        public bool Update(CartModel _cart)
        {
            if (_cart == null)
            {
                throw new ArgumentNullException(nameof(_cart), "CartModel cannot be null");
            }
            try
            {
                using (var dbContext = new CartContext())
                {
                    var existCart = dbContext.Carts.FirstOrDefault(c => c.Username == _cart.Username);
                    var cartItem = dbContext.CartItems.FirstOrDefault(c => c.ProId == _cart.ProId && c.CartId == existCart.CartId);
                    int result = 0;
                    if (cartItem != null)
                    {
                        //TODO UPDATE CART ITEM
                        cartItem.Quantity = _cart.Quantity;
                        dbContext.Entry<CartItem>(cartItem).State = EntityState.Modified;
                        result = dbContext.SaveChanges();

                        var totalPrice = dbContext.CartItems
                            .Where(c => c.CartId == existCart.CartId)
                            .Sum(c => c.Price * c.Quantity);

                        existCart.ProPrice = totalPrice;
                        dbContext.Entry<Cart>(existCart).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    return result > 0;

                }
                    
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public void DeleteCartById(string proId, string username)
        //{
        //    var id = proId.Split(',');
        //    try
        //    {
        //        using (var context = new CartContext())
        //        {

        //            foreach (var item in id)
        //            {
        //                var cart = context.Carts.FirstOrDefault(c => c.Username == username && c.ProId == item);
        //                context.Carts.Remove(cart);
        //            }
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
