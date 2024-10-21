using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetailModel>> GetAllOrderDetailList();
        public Task<List<OrderDetailModel>> GetOrderDetailsByOrderID(string Order_ID);
    }
}
