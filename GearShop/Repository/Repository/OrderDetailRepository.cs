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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public async Task<List<OrderDetailModel>> GetAllOrderDetailList()
        => await OrderDetailDAO.GetAllOrderDetailList();
    }
}
