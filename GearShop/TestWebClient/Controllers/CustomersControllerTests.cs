using BusinessObject.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WebClient.Controllers;
using Xunit;

namespace TestWebClient.Controllers
{
    public class CustomersControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IHttpContextAccessor> mockHttpContextAccessor;

        public CustomersControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockHttpContextAccessor = this.mockRepository.Create<IHttpContextAccessor>();
        }

        private CustomersController CreateCustomersController()
        {
            return new CustomersController(
                this.mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task Details_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customersController = this.CreateCustomersController();

            // Act
            var result = await customersController.Details();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProfile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customersController = this.CreateCustomersController();
            CustomerModel accountModel = new CustomerModel
            {
                Username = "tuan1703",
                Email = "abc",
                Fullname = "abc",
                Phone = "abc"
            };

            // Act
            IActionResult rsu = await customersController.UpdateProfile(
                accountModel);

            // Assert
            Assert.True(rsu.ToString().Equals("true")?true:false);
            this.mockRepository.VerifyAll();
        }
    }
}
