using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class CartModel
    {
        public string Username { get; set; } = null!;

        public string ProId { get; set; } = null!;

        public string ProName { get; set; } = null!;

        public int Quantity { get; set; }

        public double ProPrice { get; set; } 
    }

    public class UserCartData
    {
        public CartModel CartModel { get; set; }

        public ProductData Product { get; set; }
    }
    public class CartRequest
    {
        public UserCartData UserCartData { get; set; }
        public int Amount { get; set; }
    }
}
