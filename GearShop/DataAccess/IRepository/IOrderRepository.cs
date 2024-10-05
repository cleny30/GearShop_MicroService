using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderRepository
    {
        public List<OrderModel> GetOrderList();
        public int GetCompletedOrder();
        public List<OrderDataForDashboard> GetOrderListForDashboard();
        public Task<List<Tuple<string, double>>> GetTop10CustomerAsync();
        public Task<double> GetIncomeAsync();

    }
}
