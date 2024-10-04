using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {

        public List<OrderModel> GetOrderList()
        {
            List<Order> orders;
            try
            {
                var dbContext = new OrderContext();
                orders = dbContext.Orders.ToList();
                List<OrderModel> _orders = new List<OrderModel>();
                foreach (var order in orders)
                {
                    OrderModel _order = new OrderModel();
                    _order.CopyProperties(order);
                    _orders.Add(_order);
                }
                return _orders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OrderDataForDashboard> GetOrderListForDashboard()
        {
            List<Order> orders;
            try
            {
                var dbContext = new OrderContext();
                orders = dbContext.Orders.ToList();
                List<OrderDataForDashboard> _orders = new List<OrderDataForDashboard>();
                foreach (var order in orders)
                {
                    OrderDataForDashboard _order = new OrderDataForDashboard();
                    _order.CopyProperties(order);
                    _orders.Add(_order);
                }
                return _orders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int GetCompletedOrder()
        {
            var OrderList = GetOrderListForDashboard();
            var CompletedOrder = new List<OrderDataForDashboard>();
            try
            {
                CompletedOrder = OrderList.Where(o => o.Status == 4).ToList();
            }
            catch (Exception ex)
            {
                return 0;
            }

            return CompletedOrder.Count;
        }

        public List<Tuple<string, double>> GetTop10Customer()
        {
            using (var orderContext = new OrderContext())
            using (var customerContext = new CustomerContext())
            {
                // Load orders with status 4 into memory
                var orders = orderContext.Orders
                    .Where(order => order.Status == 4)
                    .Select(order => new { order.Username, order.TotalPrice })
                    .ToList();

                // Load customers into memory
                var customers = customerContext.Customers
                    .Select(customer => new { customer.Username, customer.Fullname })
                    .ToList();

                // Perform the join in memory
                var topCustomers = orders
                    .Join(customers,
                          order => order.Username,
                          customer => customer.Username,
                          (order, customer) => new { customer.Fullname, order.TotalPrice })
                    .GroupBy(x => x.Fullname)
                    .Select(g => new { Fullname = g.Key, TotalPriceSum = g.Sum(x => x.TotalPrice) })
                    .OrderByDescending(x => x.TotalPriceSum)
                    .Take(10)
                    .Select(x => Tuple.Create(x.Fullname, (double)x.TotalPriceSum))
                    .ToList();

                return topCustomers;
            }
        }
    }
}
