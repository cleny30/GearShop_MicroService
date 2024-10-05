using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DashboardAdmin.Admin_APIEndPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardAdmin
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly HttpClient client;
        public Dashboard()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            InitializeComponent();
            _ = InitializeAsync();

        }
        private async Task InitializeAsync()
        {
            await GetCompletedOrder();
            await GetIncome();
            await GetRevenue();
            await GetOutOfStockProduct();
            await GetTopProduct();
            await GetTopCustomer();
            
        }

        private async Task GetCompletedOrder()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Order.GET_COMPLETED_ORDER);
                response.EnsureSuccessStatusCode(); 
                string strData = await response.Content.ReadAsStringAsync();
                int completedOrder = JsonSerializer.Deserialize<int>(strData);
                txtCompletedOrder.Text = completedOrder.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task GetIncome()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Order.GET_INCOME);
                response.EnsureSuccessStatusCode();
                string strData = await response.Content.ReadAsStringAsync();
                int income = JsonSerializer.Deserialize<int>(strData, options);
                txtIncome.Text = income.ToString() + "$";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task GetRevenue()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Order.GET_REVENUE);
                response.EnsureSuccessStatusCode();
                string strData = await response.Content.ReadAsStringAsync();
                int revenue = JsonSerializer.Deserialize<int>(strData, options); 
                txtRevenue.Text = revenue.ToString() + "$";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task GetOutOfStockProduct()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_LIST_WITHOUT_BRAND_AND_CATEGORY);
            response.EnsureSuccessStatusCode();
            string strData = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductModel>>(strData, options);

            // Filter out products that are out of stock or nearly out of stock
            var outOfStock = products.Where(p => p.ProQuan <= 1).ToList();

            // Update the data grid's item source
            OutOfStockDataGrid.ItemsSource = outOfStock;
        }

        private async Task GetTopCustomer()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Order.GET_TOP_10_CUSTOMERS);
            response.EnsureSuccessStatusCode();
            string strData = await response.Content.ReadAsStringAsync();
            var Top10Customer = JsonSerializer.Deserialize<List<CustomerModel>>(strData, options);
            TopCustomerDataGrid.ItemsSource = Top10Customer;
        }
        public class CustomerModel
        {
            public string item1 { get; set; }
            public double item2 { get; set; }
        }

        private async Task GetTopProduct()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage response = await client.GetAsync(Admin_APIEndPoint_Order.GET_ALL_ORDER_DETAIL_LIST);
            response.EnsureSuccessStatusCode();
            string strData = await response.Content.ReadAsStringAsync();

             var orderDetails = JsonSerializer.Deserialize<List<OrderDetailModel>>(strData, options);

            // Group by ProId and select the top 10 products by quantity
            var consolidatedProducts = orderDetails
                .GroupBy(detail => detail.ProId)
                .Select(group => new OrderDetailModel
                {
                    ProId = group.Key,
                    ProName = group.First().ProName, // Take the product name from the first item in the group
                    Quantity = group.Sum(detail => detail.Quantity)
                })
                .OrderByDescending(product => product.Quantity) // Sort by quantity in descending order
                .Take(10) // Select the top 10 products
                .ToList();
            TopProductDataGrid.ItemsSource = consolidatedProducts;
        }
    }
}
