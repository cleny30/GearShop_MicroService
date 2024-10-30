using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class OrderDetailRepositoryTests
    {
        private MockRepository mockRepository;



        public OrderDetailRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private OrderDetailRepository CreateOrderDetailRepository()
        {
            return new OrderDetailRepository();
        }

        [Fact]
        public async Task GetAllOrderDetailList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();

            // Act
            var result = await orderDetailRepository.GetAllOrderDetailList();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetOrderDetailsByOrderID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderDetailRepository = this.CreateOrderDetailRepository();
            string Order_ID = null;

            // Act
            var result = await orderDetailRepository.GetOrderDetailsByOrderID(
                Order_ID);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
