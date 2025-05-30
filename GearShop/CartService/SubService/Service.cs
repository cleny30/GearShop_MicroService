﻿using BusinessObject.DTOS;
using Repository.IRepository;

namespace CartService.SubService
{
    public class Service
    {
        private readonly ICartRepository cartRepository;

        public Service(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public bool AddOrUpdateCart(UserCartData data, int amount)
        {
            var _cartModel = cartRepository.GetCartsByUsername(data.CartModel.Username);
            var quantityInStock = data.Product.ProQuan;

            if (quantityInStock >= 0)
            {
                var existingCart = _cartModel == null ? null : _cartModel.FirstOrDefault(c => c.ProId == data.Product.ProId);
                if (existingCart != null)
                {
                    var check = quantityInStock - (existingCart.Quantity + amount);
                    if (check < 0)
                    {
                        return false;
                    }

                    existingCart.Quantity += amount;
                    existingCart.ProPrice = (data.Product.ProPrice - (data.Product.ProPrice * data.Product.Discount) / 100);
                    return cartRepository.UpdateCartData(existingCart);
                }
                else
                {
                    CartModel cartModel = new CartModel()
                    {
                        Username = data.CartModel.Username,
                        ProPrice = (data.Product.ProPrice - (data.Product.ProPrice * data.Product.Discount) / 100),
                        ProId = data.Product.ProId,
                        ProName = data.Product.ProName,
                        Quantity = amount,
                    };

                    return cartRepository.AddCart(cartModel);
                }
            }
            return false;
        }
    }
}
