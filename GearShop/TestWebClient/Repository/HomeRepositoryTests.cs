using BusinessObject.DTOS;
using DataAccess.DAO;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using Xunit;

namespace TestWebClient.Repository
{
    public class HomeRepositoryTests
    {
        private Mock<IHomeRepository> mockHomeRepository;

        public HomeRepositoryTests()
        {
            this.mockHomeRepository = new Mock<IHomeRepository>(MockBehavior.Strict);
        }
        private IHomeRepository CreateHomeRepository()
        {
            return this.mockHomeRepository.Object;
        }

        [Fact]
        public void GetMouseProducts_ReturnsListOfProductData()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();
            var expectedProducts = new List<ProductData>
        {
            new ProductData { ProId = "M001", CateId = 1, BrandId = 1, ProName = "Gaming Mouse", ProQuan = 50, ProPrice = 49.99, ProDes = "High precision gaming mouse", Discount = 10, IsAvailable = true },
            new ProductData { ProId = "M002", CateId = 1, BrandId = 2, ProName = "Wireless Mouse", ProQuan = 30, ProPrice = 29.99, ProDes = "Ergonomic wireless mouse", Discount = 5, IsAvailable = true }
        };

            this.mockHomeRepository.Setup(repo => repo.GetMouseProducts()).Returns(expectedProducts);

            // Act
            var result = homeRepository.GetMouseProducts();

            // Assert
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts.First().ProName, result.First().ProName);
            this.mockHomeRepository.VerifyAll();
        }

        [Fact]
        public void GetKeyboardProducts_ReturnsListOfProductData()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();
            var expectedProducts = new List<ProductData>
        {
            new ProductData { ProId = "K001", CateId = 1, BrandId = 1, ProName = "Mechanical Keyboard", ProQuan = 100, ProPrice = 79.99, ProDes = "High-quality mechanical keyboard", Discount = 10, IsAvailable = true },
            new ProductData { ProId = "K002", CateId = 1, BrandId = 2, ProName = "Wireless Keyboard", ProQuan = 50, ProPrice = 49.99, ProDes = "Ergonomic wireless keyboard", Discount = 5, IsAvailable = true }
        };

            this.mockHomeRepository.Setup(repo => repo.GetKeyboardProducts()).Returns(expectedProducts);

            // Act
            var result = homeRepository.GetKeyboardProducts();

            // Assert
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts.First().ProName, result.First().ProName);
            this.mockHomeRepository.VerifyAll();
        }

        [Fact]
        public void HomeGetData_ReturnsListOfProductData()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();
            var expectedProducts = new List<ProductData>
        {
            new ProductData { ProId = "P001", CateId = 1, BrandId = 1, ProName = "Gaming Mouse", ProQuan = 100, ProPrice = 49.99, ProDes = "High precision gaming mouse", Discount = 10, IsAvailable = true },
            new ProductData { ProId = "P002", CateId = 1, BrandId = 2, ProName = "Mechanical Keyboard", ProQuan = 50, ProPrice = 79.99, ProDes = "Mechanical keyboard with RGB", Discount = 5, IsAvailable = true }
        };

            this.mockHomeRepository.Setup(repo => repo.HomeGetData()).Returns(expectedProducts);

            // Act
            var result = homeRepository.HomeGetData();

            // Assert
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts.First().ProName, result.First().ProName);
            this.mockHomeRepository.VerifyAll();
        }
    }
}
