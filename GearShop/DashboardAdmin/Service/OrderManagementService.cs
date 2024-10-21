using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DashboardAdmin.Admin_APIEndPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DashboardAdmin.Service
{
    public class OrderManagementService
    {
        private readonly HttpClient _client;

        public OrderManagementService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<OrderModel>> GetOrderList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync(Admin_APIEndPoint_Order.GET_ORDER_LIST);

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<OrderModel>>(strData, options);
        }

        public async Task<OrderModel> GetOrderByID(string Order_ID)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync($"{Admin_APIEndPoint_Order.GET_ORDER_BY_ID}?Order_ID={Order_ID}");

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<OrderModel>(strData, options);
        }

        public async Task<List<OrderDetailModel>> GetOrderDetailByOrderID(string Order_ID)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var response = await _client.GetAsync($"{Admin_APIEndPoint_Order.GET_ORDER_DETAIL_BY_ORDER_ID}?Order_ID={Order_ID}");

            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<OrderDetailModel>>(strData, options);
        }

        public async Task<bool> ChangeOrderStatus(string Order_ID, int Status)
        {
            OrderDataForChangingStatus order = new OrderDataForChangingStatus
            {
                OrderId = Order_ID,
                Status = Status 
            };

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var orderJson = JsonSerializer.Serialize(order, options);
            var contentOrder = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Admin_APIEndPoint_Order.CHANGE_ORDER_STATUS, contentOrder);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options); 
        }

    }
}
