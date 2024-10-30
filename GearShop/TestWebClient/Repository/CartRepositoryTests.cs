using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using Xunit;

namespace TestWebClient.Repository
{
    public class CartRepositoryTests
    {
        private MockRepository mockRepository;



        public CartRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private CartRepository CreateCartRepository()
        {
            return new CartRepository();
        }

        [Fact]
        public void AddCart_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            CartModel _cart = null;

            // Act
            var result = cartRepository.AddCart(
                _cart);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void DeleteCartById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string proId = null;
            string username = null;

            // Act
            var result = cartRepository.DeleteCartById(
                proId,
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetCarts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();

            // Act
            var result = cartRepository.GetCarts();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetCartsByUsername_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = null;

            // Act
            var result = cartRepository.GetCartsByUsername(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetUserCartDatas_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = null;
            List<string> proIds = null;

            // Act
            var result = cartRepository.GetUserCartDatas(
                username,
                proIds);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void UpdateCartData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            CartModel _cart = null;

            // Act
            var result = cartRepository.UpdateCartData(
                _cart);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
