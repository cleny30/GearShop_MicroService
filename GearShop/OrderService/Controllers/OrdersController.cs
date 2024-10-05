using BusinessObject.DTOS;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IOrderRepository _orderRepository;
        private IImportProductRepository _importProductRepository;
        private IOrderDetailRepository _orderDetailRepository;

        public OrdersController(IOrderRepository repository, IImportProductRepository importProductRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = repository;
            _importProductRepository = importProductRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet("GetOrderList")]
        public  IActionResult GetOrderList()
        {
            List<OrderModel> list = _orderRepository.GetOrderList(); // Assuming an async method
            return Ok(list);
        }

        [HttpGet("GetOrderListForDashboard")]
        public IActionResult GetOrderListForDashboard()
        {
            List<OrderDataForDashboard> list = _orderRepository.GetOrderListForDashboard();  
            return Ok(list);
        }

        [HttpGet("GetCompletedOrder")]
        public IActionResult GetCompletedOrder()
        {
            int CompletedOrder = _orderRepository.GetCompletedOrder();
            return Ok(CompletedOrder);  
        }

        [HttpGet("GetTop10Customers")]
        public async Task<IActionResult> GetTop10Customers()
        {
            List<Tuple<string, double>> Top10Customers = await _orderRepository.GetTop10CustomerAsync();
            return Ok(Top10Customers);  
        }

        [HttpGet("GetIncome")]
        public async Task<IActionResult> GetIncome()
        {
            double Income = await _orderRepository.GetIncomeAsync(); 
            return Ok(Income);  
        }

        [HttpGet("GetRevenue")]
        public async Task<IActionResult> GetRevenue()
        {
            double income = await _orderRepository.GetIncomeAsync();
            double spent = await _importProductRepository.GetMoneySpent();
            return Ok(income - spent);
        }

        [HttpGet("GetAllOrderDetailList")]
        public async Task<IActionResult> GetAllOrderDetailList()
        {
            List<OrderDetailModel> list = await _orderDetailRepository.GetAllOrderDetailList();
            return Ok(list);    
        }
    }
}
