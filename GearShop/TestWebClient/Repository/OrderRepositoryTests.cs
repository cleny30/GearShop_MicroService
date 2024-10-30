using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class OrderRepositoryTests
    {
        private Mock<IOrderRepository> mockOrderRepository;

        public OrderRepositoryTests()
        {
            this.mockOrderRepository = new Mock<IOrderRepository>(MockBehavior.Strict);
        }

        private IOrderRepository CreateOrderRepository()
        {
            return this.mockOrderRepository.Object;
        }

        [Fact]
        public async Task AddNewOrder_ValidOrder_ReturnsTrue()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var orderModel = new OrderModel
            {
                OrderId = "O001",
                ManagerId = 1,
                Username = "testuser",
                TotalPrice = 100.00,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = null,
                OrderDes = "Test order",
                Status = 1,
                Fullname = "Test User",
                Phone = "1234567890",
                Address = "123 Main St",
                Email = "testuser@example.com",
                proId = "P001"
            };

            // Setup the mock to expect the AddNewOrder call
            this.mockOrderRepository.Setup(repo => repo.AddNewOrder(orderModel)).ReturnsAsync(true);

            // Act
            var result = await orderRepository.AddNewOrder(orderModel);

            // Assert
            Assert.True(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task AddNewOrder_NullOrder_ReturnsFalse()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            OrderModel orderModel = null;

            // Setup the mock to expect the AddNewOrder call
            this.mockOrderRepository.Setup(repo => repo.AddNewOrder(orderModel)).ReturnsAsync(false);

            // Act
            var result = await orderRepository.AddNewOrder(orderModel);

            // Assert
            Assert.False(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task AddOrderDetail_ValidOrderDetails_ReturnsTrue()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var orderDetailModel = new List<OrderDetailModel>
        {
            new OrderDetailModel { OrderId = "O001", ProId = "P001", ProName = "Product 1", Quantity = 2, Price = 19.99 },
            new OrderDetailModel { OrderId = "O001", ProId = "P002", ProName = "Product 2", Quantity = 1, Price = 29.99 }
        };

            // Setup the mock to expect the AddOrderDetail call
            this.mockOrderRepository.Setup(repo => repo.AddOrderDetail(orderDetailModel)).ReturnsAsync(true);

            // Act
            var result = await orderRepository.AddOrderDetail(orderDetailModel);

            // Assert
            Assert.True(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task AddOrderDetail_NullOrderDetails_ReturnsFalse()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            List<OrderDetailModel> orderDetailModel = null;

            // Setup the mock to expect the AddOrderDetail call
            this.mockOrderRepository.Setup(repo => repo.AddOrderDetail(orderDetailModel)).ReturnsAsync(false);

            // Act
            var result = await orderRepository.AddOrderDetail(orderDetailModel);

            // Assert
            Assert.False(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeOrderStatus_ValidOrderIdAndStatus_ReturnsTrue()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = "O001"; // Use a valid order ID for testing
            int status = 1; // Example status

            // Setup the mock to expect the ChangeOrderStatus call
            this.mockOrderRepository.Setup(repo => repo.ChangeOrderStatus(orderId, status)).ReturnsAsync(true);

            // Act
            var result = await orderRepository.ChangeOrderStatus(orderId, status);

            // Assert
            Assert.True(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeOrderStatus_InvalidOrderId_ReturnsFalse()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = "InvalidOrderId"; // Use an invalid order ID
            int status = 0; // Example status

            // Setup the mock to expect the ChangeOrderStatus call
            this.mockOrderRepository.Setup(repo => repo.ChangeOrderStatus(orderId, status)).ReturnsAsync(false);

            // Act
            var result = await orderRepository.ChangeOrderStatus(orderId, status);

            // Assert
            Assert.False(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeOrderStatus_NullOrderId_ReturnsFalse()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = null; // Null order ID
            int status = 0; // Example status

            // Setup the mock to expect the ChangeOrderStatus call
            this.mockOrderRepository.Setup(repo => repo.ChangeOrderStatus(orderId, status)).ReturnsAsync(false);

            // Act
            var result = await orderRepository.ChangeOrderStatus(orderId, status);

            // Assert
            Assert.False(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public void GetCompletedOrder_ReturnsCountOfCompletedOrders()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            int expectedCount = 5; // Example expected count of completed orders

            // Setup the mock to expect the GetCompletedOrder call
            this.mockOrderRepository.Setup(repo => repo.GetCompletedOrder()).Returns(expectedCount);

            // Act
            var result = orderRepository.GetCompletedOrder();

            // Assert
            Assert.Equal(expectedCount, result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetIncomeAsync_ReturnsTotalIncome()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            double expectedIncome = 1500.00; // Example total income

            // Setup the mock to expect the GetIncomeAsync call
            this.mockOrderRepository.Setup(repo => repo.GetIncomeAsync()).ReturnsAsync(expectedIncome);

            // Act
            var result = await orderRepository.GetIncomeAsync();

            // Assert
            Assert.Equal(expectedIncome, result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewOrderId_ReturnsNewOrderId()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string expectedOrderId = "OD004"; // Example expected order ID

            // Setup the mock to expect the GetNewOrderId call
            this.mockOrderRepository.Setup(repo => repo.GetNewOrderId()).ReturnsAsync(expectedOrderId);

            // Act
            var result = await orderRepository.GetNewOrderId();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderId, result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewOrderId_ReturnsEmptyString_WhenNoIdAvailable()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string expectedOrderId = string.Empty; // Example expected order ID when none is available

            // Setup the mock to expect the GetNewOrderId call
            this.mockOrderRepository.Setup(repo => repo.GetNewOrderId()).ReturnsAsync(expectedOrderId);

            // Act
            var result = await orderRepository.GetNewOrderId();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderId, result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderByID_ValidOrderId_ReturnsOrderModel()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = "O001"; // Use a valid order ID for testing
            var expectedOrder = new OrderModel
            {
                OrderId = orderId,
                ManagerId = 1,
                Username = "testuser",
                TotalPrice = 100.00,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = null,
                OrderDes = "Test order",
                Status = 1,
                Fullname = "Test User",
                Phone = "1234567890",
                Address = "123 Main St",
                Email = "testuser@example.com",
                proId = "P001"
            };

            // Setup the mock to expect the GetOrderByID call
            this.mockOrderRepository.Setup(repo => repo.GetOrderByID(orderId)).ReturnsAsync(expectedOrder);

            // Act
            var result = await orderRepository.GetOrderByID(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrder.OrderId, result.OrderId);
            Assert.Equal(expectedOrder.Username, result.Username);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderByID_InvalidOrderId_ReturnsNull()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = "InvalidOrderId"; // Use an invalid order ID

            // Setup the mock to expect the GetOrderByID call
            this.mockOrderRepository.Setup(repo => repo.GetOrderByID(orderId)).ReturnsAsync((OrderModel)null);

            // Act
            var result = await orderRepository.GetOrderByID(orderId);

            // Assert
            Assert.Null(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderByID_NullOrderId_ReturnsNull()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string orderId = null; // Null order ID

            // Setup the mock to expect the GetOrderByID call
            this.mockOrderRepository.Setup(repo => repo.GetOrderByID(orderId)).ReturnsAsync((OrderModel)null);

            // Act
            var result = await orderRepository.GetOrderByID(orderId);

            // Assert
            Assert.Null(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderList_ReturnsListOfOrderModels()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedOrders = new List<OrderModel>
        {
            new OrderModel { OrderId = "O001", ManagerId = 1, Username = "testuser1", TotalPrice = 100.00, StartDate = DateOnly.FromDateTime(DateTime.Now), Status = 1, Fullname = "User One", Phone = "1234567890", Address = "123 Main St" },
            new OrderModel { OrderId = "O002", ManagerId = 2, Username = "testuser2", TotalPrice = 200.00, StartDate = DateOnly.FromDateTime(DateTime.Now), Status = 1, Fullname = "User Two", Phone = "0987654321", Address = "456 Elm St" }
        };

            // Setup the mock to expect the GetOrderList call
            this.mockOrderRepository.Setup(repo => repo.GetOrderList()).Returns(expectedOrders);

            // Act
            var result = orderRepository.GetOrderList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrders.Count, result.Count);
            Assert.Equal(expectedOrders[0].OrderId, result[0].OrderId);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderList_ReturnsEmptyList_WhenNoOrders()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedOrders = new List<OrderModel>(); // Empty list

            // Setup the mock to expect the GetOrderList call
            this.mockOrderRepository.Setup(repo => repo.GetOrderList()).Returns(expectedOrders);

            // Act
            var result = orderRepository.GetOrderList();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderListForDashboard_ReturnsListOfOrderDataForDashboard()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedOrders = new List<OrderDataForDashboard>
        {
            new OrderDataForDashboard { OrderId = "O001", Status = 1 },
            new OrderDataForDashboard { OrderId = "O002", Status = 2 }
        };

            // Setup the mock to expect the GetOrderListForDashboard call
            this.mockOrderRepository.Setup(repo => repo.GetOrderListForDashboard()).Returns(expectedOrders);

            // Act
            var result = orderRepository.GetOrderListForDashboard();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrders.Count, result.Count);
            Assert.Equal(expectedOrders[0].OrderId, result[0].OrderId);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderListForDashboard_ReturnsEmptyList_WhenNoOrders()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedOrders = new List<OrderDataForDashboard>(); // Empty list

            // Setup the mock to expect the GetOrderListForDashboard call
            this.mockOrderRepository.Setup(repo => repo.GetOrderListForDashboard()).Returns(expectedOrders);

            // Act
            var result = orderRepository.GetOrderListForDashboard();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrdersByCustomer_ValidUsername_ReturnsListOfOrderDataModels()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string username = "testuser";
            var expectedOrders = new List<OrderDataModel>
        {
            new OrderDataModel { OrderId = "O001", UserName = username, TotalPrice = 100.00, StartDate = DateOnly.FromDateTime(DateTime.Now), Status = 1, Address = "123 Main St" },
            new OrderDataModel { OrderId = "O002", UserName = username, TotalPrice = 200.00, StartDate = DateOnly.FromDateTime(DateTime.Now), Status = 1, Address = "456 Elm St" }
        };

            // Setup the mock to expect the GetOrdersByCustomer call
            this.mockOrderRepository.Setup(repo => repo.GetOrdersByCustomer(username)).ReturnsAsync(expectedOrders);

            // Act
            var result = await orderRepository.GetOrdersByCustomer(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrders.Count, result.Count);
            Assert.Equal(expectedOrders[0].OrderId, result[0].OrderId);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrdersByCustomer_NullUsername_ReturnsEmptyList()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string username = null; // Null username

            // Setup the mock to expect the GetOrdersByCustomer call
            this.mockOrderRepository.Setup(repo => repo.GetOrdersByCustomer(username)).ReturnsAsync(new List<OrderDataModel>());

            // Act
            var result = await orderRepository.GetOrdersByCustomer(username);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetTop10CustomerAsync_ReturnsListOfTopCustomers()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedTopCustomers = new List<Tuple<string, double>>
        {
            new Tuple<string, double>("customer1", 1500.00),
            new Tuple<string, double>("customer2", 1200.00),
            new Tuple<string, double>("customer3", 900.00),
            new Tuple<string, double>("customer4", 800.00),
            new Tuple<string, double>("customer5", 700.00),
            new Tuple<string, double>("customer6", 600.00),
            new Tuple<string, double>("customer7", 500.00),
            new Tuple<string, double>("customer8", 400.00),
            new Tuple<string, double>("customer9", 300.00),
            new Tuple<string, double>("customer10", 200.00)
        };

            // Setup the mock to expect the GetTop10CustomerAsync call
            this.mockOrderRepository.Setup(repo => repo.GetTop10CustomerAsync()).ReturnsAsync(expectedTopCustomers);

            // Act
            var result = await orderRepository.GetTop10CustomerAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTopCustomers.Count, result.Count);
            Assert.Equal(expectedTopCustomers[0].Item1, result[0].Item1); // Check customer name
            Assert.Equal(expectedTopCustomers[0].Item2, result[0].Item2); // Check total spending
            this.mockOrderRepository.VerifyAll();
        }

        [Fact]
        public async Task GetTop10CustomerAsync_ReturnsEmptyList_WhenNoCustomers()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            var expectedTopCustomers = new List<Tuple<string, double>>(); // Empty list

            // Setup the mock to expect the GetTop10CustomerAsync call
            this.mockOrderRepository.Setup(repo => repo.GetTop10CustomerAsync()).ReturnsAsync(expectedTopCustomers);

            // Act
            var result = await orderRepository.GetTop10CustomerAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderRepository.VerifyAll();
        }
    }
}
