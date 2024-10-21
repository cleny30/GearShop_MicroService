using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetailModel>> GetAllOrderDetailList();
        public Task<List<OrderDetailModel>> GetOrderDetailsByOrderID(string Order_ID);
    }
}
