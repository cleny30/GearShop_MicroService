using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class OrderDetailRepositoryTests
    {
        private Mock<IOrderDetailRepository> mockOrderDetailRepository;

        public OrderDetailRepositoryTests()
        {
            this.mockOrderDetailRepository = new Mock<IOrderDetailRepository>(MockBehavior.Strict);
        }

        private IOrderDetailRepository CreateOrderDetailRepository()
        {
            return this.mockOrderDetailRepository.Object;
        }


        [Fact]
        public async Task GetAllOrderDetailList_ReturnsListOfOrderDetailModels()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();
            var expectedOrderDetails = new List<OrderDetailModel>
        {
            new OrderDetailModel { OrderId = "OD001", ProId = "P001", ProName = "Product 1", Quantity = 2, Price = 19.99 },
            new OrderDetailModel { OrderId = "OD002", ProId = "P002", ProName = "Product 2", Quantity = 1, Price = 29.99 }
        };

            // Setup the mock to expect the GetAllOrderDetailList call
            this.mockOrderDetailRepository.Setup(repo => repo.GetAllOrderDetailList()).ReturnsAsync(expectedOrderDetails);

            // Act
            var result = await orderDetailRepository.GetAllOrderDetailList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderDetails.Count, result.Count);
            Assert.Equal(expectedOrderDetails[0].ProName, result[0].ProName);
            this.mockOrderDetailRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAllOrderDetailList_ReturnsEmptyList_WhenNoOrderDetails()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();
            var expectedOrderDetails = new List<OrderDetailModel>(); // Empty list

            // Setup the mock to expect the GetAllOrderDetailList call
            this.mockOrderDetailRepository.Setup(repo => repo.GetAllOrderDetailList()).ReturnsAsync(expectedOrderDetails);

            // Act
            var result = await orderDetailRepository.GetAllOrderDetailList();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderDetailRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderDetailsByOrderID_ValidOrderId_ReturnsListOfOrderDetailModels()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();
            string orderId = "O001"; // Use a valid order ID for testing
            var expectedOrderDetails = new List<OrderDetailModel>
        {
            new OrderDetailModel { OrderId = orderId, ProId = "P001", ProName = "Product 1", Quantity = 2, Price = 19.99 },
            new OrderDetailModel { OrderId = orderId, ProId = "P002", ProName = "Product 2", Quantity = 1, Price = 29.99 }
        };

            // Setup the mock to expect the GetOrderDetailsByOrderID call
            this.mockOrderDetailRepository.Setup(repo => repo.GetOrderDetailsByOrderID(orderId)).ReturnsAsync(expectedOrderDetails);

            // Act
            var result = await orderDetailRepository.GetOrderDetailsByOrderID(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderDetails.Count, result.Count);
            Assert.Equal(expectedOrderDetails[0].ProName, result[0].ProName);
            this.mockOrderDetailRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderDetailsByOrderID_InvalidOrderId_ReturnsEmptyList()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();
            string orderId = "InvalidOrderId"; // Use an invalid order ID

            // Setup the mock to expect the GetOrderDetailsByOrderID call
            this.mockOrderDetailRepository.Setup(repo => repo.GetOrderDetailsByOrderID(orderId)).ReturnsAsync(new List<OrderDetailModel>());

            // Act
            var result = await orderDetailRepository.GetOrderDetailsByOrderID(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockOrderDetailRepository.VerifyAll();
        }
    }
}
