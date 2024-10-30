using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

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

        public static async Task<OrderModel> GetOrderByID(string Order_ID)
        {
            OrderModel _order = new OrderModel();
            try
            {
                using (var dbContext = new OrderContext())
                {
                    Order order = await dbContext.Orders.
                        FirstOrDefaultAsync(o => o.OrderId.Equals(Order_ID));
                    _order.CopyProperties(order);
                    return _order;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<bool> ChangeOrderStatus(string Order_ID, int Status)
        {
            Order order = new Order();
            try
            {
                using (var dbContext = new OrderContext())
                {
                    order = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(Order_ID));
                    order.Status = Status;
                    if (Status == 4)
                    {
                        order.EndDate = DateOnly.FromDateTime(DateTime.Now);

                    }

                    if (Status == 0)
                    {
                        List<OrderDetail> list = new List<OrderDetail>();

                        list = await dbContext.OrderDetails.Where(o => o.OrderId.Equals(order.OrderId)).ToListAsync();

                        using (var productContext = new ProductContext())
                        {
                            foreach (var item in list)
                            {
                                Product product = productContext.Products.FirstOrDefault(p => p.ProId.Equals(item.ProId));
                                product.ProQuan += item.Quantity;
                                productContext.SaveChanges();
                            }
                        }
                        
                    }
                    dbContext.Entry<Order>(order).State = EntityState.Modified;
                    int check = await dbContext.SaveChangesAsync();
                    if (check > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<string> GetNewOrderID()
        {
            using (var dbContext = new OrderContext())
            {
                var lastOrder = await dbContext.Orders.OrderByDescending(o => o.OrderId).FirstOrDefaultAsync();

                if (lastOrder == null)
                {
                    return "OD001"; // Assuming the first order ID starts with "OD001"
                }
                else
                {
                    string numericPart = lastOrder.OrderId.Substring(2);
                    int currentNumber = int.Parse(numericPart);
                    int newNumber = currentNumber + 1;
                    string newOrderId = $"OD{newNumber:D3}";
                    return newOrderId;
                }
            }
        }

        public static async Task<bool> AddNewOrder(OrderModel orderModel)
        {
            try
            {
                using (var dbContext = new OrderContext())
                {
                    Order order = new Order();
                    order.CopyProperties(orderModel);
                    dbContext.Orders.Add(order);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> AddOrderDetail(OrderDetailModel orderDetailModel)
        {
            try
            {
                using (var dbContext = new OrderContext())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.CopyProperties(orderDetailModel);
                    dbContext.OrderDetails.Add(orderDetail);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async static Task<List<OrderDataModel>> GetOrderListByUser(string username)
        {
            List<Order> orders;
            try
            {
                using (var dbContext = new OrderContext())
                {
                    orders = await dbContext.Orders.Where(o => o.Username == username).ToListAsync();
                    List<OrderDataModel> _orders = new List<OrderDataModel>();
                    foreach (var order in orders)
                    {
                        OrderDataModel _order = new OrderDataModel();
                        _order.CopyProperties(order);
                        _orders.Add(_order);
                    }
                    return _orders;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
