using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        private static DateTime currentDate = DateTime.Now;
        public static List<OrderModel> GetOrderList()
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
        public static List<OrderDataForDashboard> GetOrderListForDashboard()
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
        public static int GetCompletedOrder()
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
        public static async Task<List<Tuple<string, double>>> GetTop10CustomerAsync()
        {
            using (var orderContext = new OrderContext())
            using (var customerContext = new CustomerContext())
            {
                // Load orders with status 4 into memory asynchronously
                var orders = await orderContext.Orders
                    .Where(order => order.Status == 4)
                    .Select(order => new { order.Username, order.TotalPrice })
                    .ToListAsync();

                // Load customers into memory asynchronously
                var customers = await customerContext.Customers
                    .Select(customer => new { customer.Username, customer.Fullname })
                    .ToListAsync();

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
        public static async Task<double> GetIncomeAsync()
        {
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;

            // Assuming GetOrderListAsync is an asynchronous method
            var orders = (GetOrderList())?.ToList() ?? new List<OrderModel>();

            var incomeList = orders
                .Where(o => o.Status == 4 && o.EndDate != null &&
                            o.EndDate.Value.Year == currentYear && o.EndDate.Value.Month == currentMonth)
                .Select(o => o.TotalPrice)
                .ToList();

            return incomeList.Any() ? incomeList.Sum() : 0;
        }
    }
}
