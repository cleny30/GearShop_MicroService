using DataAccess.DAO;
using Moq;
using Repository.Repository;
using System;
using Xunit;

namespace TestWebClient.Repository
{
    public class HomeRepositoryTests
    {
        private MockRepository mockRepository;

        private Mock<HomeDAO> mockHomeDAO;

        public HomeRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockHomeDAO = this.mockRepository.Create<HomeDAO>();
        }

        private HomeRepository CreateHomeRepository()
        {
            return new HomeRepository(
                this.mockHomeDAO.Object);
        }

        [Fact]
        public void GetMouseProducts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();

            // Act
            var result = homeRepository.GetMouseProducts();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetKeyboardProducts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();

            // Act
            var result = homeRepository.GetKeyboardProducts();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void HomeGetData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeRepository = this.CreateHomeRepository();

            // Act
            var result = homeRepository.HomeGetData();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
