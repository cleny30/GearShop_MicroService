using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public async Task<List<OrderDetailModel>> GetAllOrderDetailList()
        => await OrderDetailDAO.GetAllOrderDetailList();

        public async Task<List<OrderDetailModel>> GetOrderDetailsByOrderID(string Order_ID)
        => await OrderDetailDAO.GetOrderDetailsByOrderID(Order_ID);
    }
}
