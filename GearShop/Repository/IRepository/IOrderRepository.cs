using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        public List<OrderModel> GetOrderList();
        public Task<List<OrderDataModel>> GetOrdersByCustomer(string username);
        public int GetCompletedOrder();
        public List<OrderDataForDashboard> GetOrderListForDashboard();
        public Task<List<Tuple<string, double>>> GetTop10CustomerAsync();
        public Task<double> GetIncomeAsync();
        public Task<OrderModel> GetOrderByID(string Order_ID);
        public Task<bool> ChangeOrderStatus(string Order_ID, int Status);

        public Task<string> GetNewOrderId();
        public Task<bool> AddOrderDetail(List<OrderDetailModel> orderDetailModel);
        public Task<bool> AddNewOrder(OrderModel orderModel);
    }
}
