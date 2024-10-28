using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetOrderByUsername")]
        public async Task<IActionResult> GetOrderByUsername(string username)
        {
            List<OrderDataModel> list =  await _orderRepository.GetOrdersByCustomer(username);
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

        [HttpGet("GetOrderByID")]
        public async Task<IActionResult> GetOrderByID(string Order_ID)
        {
            OrderModel order = await _orderRepository.GetOrderByID(Order_ID);
            return Ok(order);
        }

        [HttpGet("GetOrderDetailByOrderID")]
        public async Task<IActionResult> GetOrderDetailByOrderID(string Order_ID)
        {
            List<OrderDetailModel> list = 
                await _orderDetailRepository.GetOrderDetailsByOrderID(Order_ID);
            return Ok(list);
        }

        [HttpPut("ChangeOrderStatus")]
        public async Task<IActionResult> ChangeOrderStatus(OrderDataForChangingStatus order)
        {
            var isSuccessful = await _orderRepository.ChangeOrderStatus(order.OrderId, order.Status);
            return Ok(isSuccessful);
        }

        [HttpGet("GetNewOrderID")]
        public async Task<IActionResult> GetNewOrderID()
        {
            string orderId = await _orderRepository.GetNewOrderId();
            return Ok(orderId);
        }

        [HttpPost("AddOrderModel")]
        public async Task<IActionResult> AddOrder([FromBody]OrderModel model)
        {
            bool result = await _orderRepository.AddNewOrder(model);
            return Ok(result);
        }

        [HttpPost("AddOrderDetails")]
        public async Task<IActionResult> AddOrderDetails(List<OrderDetailModel> model)
        {
            return Ok(await _orderRepository.AddOrderDetail(model));
        }
    }
}
