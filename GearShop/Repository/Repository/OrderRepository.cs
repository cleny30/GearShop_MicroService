using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<bool> ChangeOrderStatus(string Order_ID, int Status)
        => await OrderDAO.ChangeOrderStatus(Order_ID, Status);

        public int GetCompletedOrder()
        => OrderDAO.GetCompletedOrder();

        public async Task<double> GetIncomeAsync()
        => await OrderDAO.GetIncomeAsync();

        public async Task<OrderModel> GetOrderByID(string Order_ID)
        => await OrderDAO.GetOrderByID(Order_ID);

        public List<OrderModel> GetOrderList()
        => OrderDAO.GetOrderList(); 

        public List<OrderDataForDashboard> GetOrderListForDashboard()
        => OrderDAO.GetOrderListForDashboard();

        public async Task<List<Tuple<string, double>>> GetTop10CustomerAsync()
        => await OrderDAO.GetTop10CustomerAsync();
    }
}
