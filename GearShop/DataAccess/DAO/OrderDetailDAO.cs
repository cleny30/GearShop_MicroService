using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        public static async Task<List<OrderDetailModel>> GetAllOrderDetailList()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                using (var dbContext = new OrderContext())
                {
                    orderDetails = await dbContext.OrderDetails.ToListAsync(); 
                }

                List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();
                foreach (var orderDetail in orderDetails)
                {
                    OrderDetailModel _orderDetail = new OrderDetailModel();
                    _orderDetail.CopyProperties(orderDetail);
                    _orderDetails.Add(_orderDetail);
                }

                return _orderDetails;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return null;
            }
        }

        public static async Task<List<OrderDetailModel>> GetOrderDetailsByOrderID(string Order_ID)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                using (var dbContext = new OrderContext())
                {
                    orderDetails = await dbContext.
                        OrderDetails.Where(o => o.OrderId.Equals(Order_ID)).ToListAsync();
                }

                List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();
                foreach (var orderDetail in orderDetails)
                {
                    OrderDetailModel _orderDetail = new OrderDetailModel();
                    _orderDetail.CopyProperties(orderDetail);
                    _orderDetails.Add(_orderDetail);
                }

                return _orderDetails;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return null;
            }
        }
    }
}
