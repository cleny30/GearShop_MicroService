using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        public List<OrderModel> GetOrderList();
        public int GetCompletedOrder();
        public List<OrderDataForDashboard> GetOrderListForDashboard();
        public Task<List<Tuple<string, double>>> GetTop10CustomerAsync();
        public Task<double> GetIncomeAsync();
        public Task<OrderModel> GetOrderByID(string Order_ID);
        public Task<bool> ChangeOrderStatus(string Order_ID, int Status);
    }
}
