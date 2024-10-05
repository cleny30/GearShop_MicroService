using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public int GetCompletedOrder()
        => OrderDAO.GetCompletedOrder();

        public async Task<double> GetIncomeAsync()
        => await OrderDAO.GetIncomeAsync();

        public List<OrderModel> GetOrderList()
        => OrderDAO.GetOrderList(); 

        public List<OrderDataForDashboard> GetOrderListForDashboard()
        => OrderDAO.GetOrderListForDashboard();

        public async Task<List<Tuple<string, double>>> GetTop10CustomerAsync()
        => await OrderDAO.GetTop10CustomerAsync();
    }
}
