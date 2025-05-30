﻿using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                using (var dbContext = new CartContext())
                {
                    var cart = dbContext.Carts.FirstOrDefault(u => u.Username == username);

                    if(cart != null)
                    {
                        var cartItems = dbContext.CartItems.Where(c => c.CartId == cart.CartId);

                        List<CartModel> CartModels = new List<CartModel>();

                        foreach (var item in cartItems)
                        {
                            CartModel CartModel = new CartModel();
                            CartModel.CopyProperties(item);
                            CartModel.ProPrice = item.Price;
                            CartModel.Username = item.Cart.Username;
                            CartModels.Add(CartModel);
                        }
                        return CartModels.Where(u => u.Username == username).ToList();
                    }
                    return null;
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

        public bool DeleteCartById(string proId, string username)
        {
            try
            {
                var id = proId.Split(',');
                using (var dbContext = new CartContext())
                {
                    var existCart = dbContext.Carts.FirstOrDefault(c => c.Username == username);

                    foreach (var item in id)
                    {
                        var cart = dbContext.CartItems.FirstOrDefault(c => c.CartId == existCart.CartId && c.ProId == item);
                        dbContext.CartItems.Remove(cart);
                    }

                    dbContext.SaveChanges();

                    if(dbContext.CartItems.FirstOrDefault(c => c.CartId == existCart.CartId) == null)
                    {
                        dbContext.Carts.Remove(existCart);
                    }
                    int result = dbContext.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
