using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class OrderRepositoryTests
    {
        private MockRepository mockRepository;



        public OrderRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private OrderRepository CreateOrderRepository()
        {
            return new OrderRepository();
        }

        [Fact]
        public async Task AddNewOrder_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            OrderModel orderModel = null;

            // Act
            var result = await orderRepository.AddNewOrder(
                orderModel);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddOrderDetail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            List<OrderDetailModel> orderDetailModel = null;

            // Act
            var result = await orderRepository.AddOrderDetail(
                orderDetailModel);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeOrderStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string Order_ID = null;
            int Status = 0;

            // Act
            var result = await orderRepository.ChangeOrderStatus(
                Order_ID,
                Status);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetCompletedOrder_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = orderRepository.GetCompletedOrder();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetIncomeAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = await orderRepository.GetIncomeAsync();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewOrderId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = await orderRepository.GetNewOrderId();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string Order_ID = null;

            // Act
            var result = await orderRepository.GetOrderByID(
                Order_ID);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = orderRepository.GetOrderList();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetOrderListForDashboard_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = orderRepository.GetOrderListForDashboard();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrdersByCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();
            string username = null;

            // Act
            var result = await orderRepository.GetOrdersByCustomer(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetTop10CustomerAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderRepository = this.CreateOrderRepository();

            // Act
            var result = await orderRepository.GetTop10CustomerAsync();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
