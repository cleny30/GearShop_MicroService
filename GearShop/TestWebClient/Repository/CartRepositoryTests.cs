using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using Xunit;

namespace TestWebClient.Repository
{
    public class CartRepositoryTests
    {
        private Mock<ICartRepository> mockCartRepository;



        public CartRepositoryTests()
        {
            this.mockCartRepository = new Mock<ICartRepository>(MockBehavior.Strict);


        }

        private ICartRepository CreateCartRepository()
        {
            return this.mockCartRepository.Object;
        }

        [Fact]
        public void AddCart_ValidCart_ReturnsTrue()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            var cart = new CartModel
            {
                Username = "testuser",
                ProId = "P123",
                ProName = "Product Name",
                Quantity = 2,
                ProPrice = 19.99
            };

            this.mockCartRepository.Setup(repo => repo.AddCart(cart)).Returns(true);

            // Act
            var result = cartRepository.AddCart(cart);

            // Assert
            Assert.True(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void AddCart_NullCart_ReturnsFalse()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            CartModel cart = null;

            this.mockCartRepository.Setup(repo => repo.AddCart(cart)).Returns(false);

            // Act
            var result = cartRepository.AddCart(cart);

            // Assert
            Assert.False(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void DeleteCartById_ValidProIdAndUsername_ReturnsTrue()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string proId = "CT001";
            string username = "testuser";

            this.mockCartRepository.Setup(repo => repo.DeleteCartById(proId, username)).Returns(true);

            // Act
            var result = cartRepository.DeleteCartById(proId, username);

            // Assert
            Assert.True(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void DeleteCartById_NullProIdOrUsername_ReturnsFalse()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string proId = null;
            string username = null;

            this.mockCartRepository.Setup(repo => repo.DeleteCartById(proId, username)).Returns(false);

            // Act
            var result = cartRepository.DeleteCartById(proId, username);

            // Assert
            Assert.False(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void GetCarts_ReturnsListOfCartModels()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            var expectedCarts = new List<CartModel>
        {
            new CartModel { Username = "user1", ProId = "CT001", ProName = "Product 1", Quantity = 2, ProPrice = 19.99 },
            new CartModel { Username = "user2", ProId = "CT002", ProName = "Product 2", Quantity = 1, ProPrice = 29.99 }
        };

            this.mockCartRepository.Setup(repo => repo.GetCarts()).Returns(expectedCarts);

            // Act
            var result = cartRepository.GetCarts();

            // Assert
            Assert.Equal(expectedCarts.Count, result.Count);
            Assert.Equal(expectedCarts.First().ProName, result.First().ProName);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void GetCartsByUsername_ValidUsername_ReturnsListOfCartModels()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = "user1";
            var expectedCarts = new List<CartModel>
        {
            new CartModel { Username = "user1", ProId = "P123", ProName = "Product 1", Quantity = 2, ProPrice = 19.99 },
            new CartModel { Username = "user1", ProId = "P456", ProName = "Product 2", Quantity = 1, ProPrice = 29.99 }
        };

            this.mockCartRepository.Setup(repo => repo.GetCartsByUsername(username)).Returns(expectedCarts);

            // Act
            var result = cartRepository.GetCartsByUsername(username);

            // Assert
            Assert.Equal(expectedCarts.Count, result.Count);
            Assert.All(result, item => Assert.Equal(username, item.Username));
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void GetCartsByUsername_NullUsername_ReturnsEmptyList()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = null;
            var expectedCarts = new List<CartModel>(); // Assuming null username returns an empty list

            this.mockCartRepository.Setup(repo => repo.GetCartsByUsername(username)).Returns(expectedCarts);

            // Act
            var result = cartRepository.GetCartsByUsername(username);

            // Assert
            Assert.Empty(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void GetUserCartDatas_ValidUsernameAndProIds_ReturnsListOfUserCartData()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = "user1";
            List<string> proIds = new List<string> { "CT001", "MS002" };
            var expectedUserCartData = new List<UserCartData>
        {
            new UserCartData
            {
                CartModel = new CartModel { Username = "user1", ProId = "CT001", ProName = "Product 1", Quantity = 2, ProPrice = 19.99 },
                Product = new ProductData { ProId = "CT001", CateId = 1, BrandId = 1, ProName = "Product 1", ProQuan = 100, ProPrice = 19.99, ProDes = "Description", Discount = 10, IsAvailable = true }
            },
            new UserCartData
            {
                CartModel = new CartModel { Username = "user1", ProId = "MS002", ProName = "Product 2", Quantity = 1, ProPrice = 29.99 },
                Product = new ProductData { ProId = "MS002", CateId = 2, BrandId = 2, ProName = "Product 2", ProQuan = 50, ProPrice = 29.99, ProDes = "Description", Discount = 5, IsAvailable = true }
            }
        };

            this.mockCartRepository.Setup(repo => repo.GetUserCartDatas(username, proIds)).Returns(expectedUserCartData);

            // Act
            var result = cartRepository.GetUserCartDatas(username, proIds);

            // Assert
            Assert.Equal(expectedUserCartData.Count, result.Count);
            Assert.Equal(expectedUserCartData.First().CartModel.ProId, result.First().CartModel.ProId);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void GetUserCartDatas_NullUsernameOrProIds_ReturnsEmptyList()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            string username = null;
            List<string> proIds = null;
            var expectedUserCartData = new List<UserCartData>(); // Assuming null inputs return an empty list

            this.mockCartRepository.Setup(repo => repo.GetUserCartDatas(username, proIds)).Returns(expectedUserCartData);

            // Act
            var result = cartRepository.GetUserCartDatas(username, proIds);

            // Assert
            Assert.Empty(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void UpdateCartData_ValidCart_ReturnsTrue()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            var cart = new CartModel
            {
                Username = "testuser",
                ProId = "CT001",
                ProName = "Product Name",
                Quantity = 3,
                ProPrice = 19.99
            };

            this.mockCartRepository.Setup(repo => repo.UpdateCartData(cart)).Returns(true);

            // Act
            var result = cartRepository.UpdateCartData(cart);

            // Assert
            Assert.True(result);
            this.mockCartRepository.VerifyAll();
        }

        [Fact]
        public void UpdateCartData_NullCart_ReturnsFalse()
        {
            // Arrange
            var cartRepository = this.CreateCartRepository();
            CartModel cart = null;

            this.mockCartRepository.Setup(repo => repo.UpdateCartData(cart)).Returns(false);

            // Act
            var result = cartRepository.UpdateCartData(cart);

            // Assert
            Assert.False(result);
            this.mockCartRepository.VerifyAll();
        }
    }
}
