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
                return CartModels.Where(u => u.Username == username).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddOrUpdateCart(CartModel _cart)
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
                        //TODO ADD
                        Cart cart = new Cart();
                        cart.Username = _cart.Username;
                        cart.ProPrice = _cart.ProPrice * _cart.Quantity;

                        dbContext.Carts.Add(cart);
                        dbContext.SaveChanges();
                        // Use the CartId from the cart object directly
                        CartItem item = new CartItem();
                        item.CopyProperties(_cart);
                        item.CartId = cart.CartId;
                        item.Price = _cart.ProPrice;
                        dbContext.CartItems.Add(item);
                        result = dbContext.SaveChanges();
                        return result > 0;
                    }
                    else
                    {
                        //TODO UPDATE CART

                        var c = dbContext.CartItems.FirstOrDefault(c => c.ProId == _cart.ProId);
                        
                        if(c == null)
                        {
                            //TODO ADD NEW ROW FOR CART ITEM
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
                        else
                        {
                            //TODO UPDATE CART ITEM
                            c.Quantity = _cart.Quantity;
                            dbContext.Entry<CartItem>(c).State = EntityState.Modified;
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
            }
            catch (Exception ex)
            {
                // Log the exception using a logging framework
                // Example: logger.LogError(ex, "An error occurred while adding a cart");
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool UpdateCartData(CartModel _cart)
        {
            Cart cart = new Cart();
            cart.CopyProperties(_cart);
            try
            {
                var dbContext = new CartContext();
                dbContext.Entry<Cart>(cart).State = EntityState.Modified;
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
