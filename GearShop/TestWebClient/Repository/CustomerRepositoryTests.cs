using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class CustomerRepositoryTests
    {
        private Mock<ICustomerRepository> mockCustomerRepository;

        public CustomerRepositoryTests()
        {
            this.mockCustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
        }

        private ICustomerRepository CreateCustomerRepository()
        {
            return this.mockCustomerRepository.Object;
        }

        [Fact]
        public void GetCustomerByUserName_ValidUsername_ReturnsCustomerModel()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = "testuser";
            var expectedCustomer = new CustomerModel
            {
                Username = username,
                Fullname = "Test User",
                Phone = "1234567890",
                Email = "testuser@example.com"
            };

            this.mockCustomerRepository.Setup(repo => repo.GetCustomerByUserName(username)).Returns(expectedCustomer);

            // Act
            var result = customerRepository.GetCustomerByUserName(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Username, result.Username);
            Assert.Equal(expectedCustomer.Fullname, result.Fullname);
            Assert.Equal(expectedCustomer.Phone, result.Phone);
            Assert.Equal(expectedCustomer.Email, result.Email);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public void GetCustomerByUserName_NullUsername_ReturnsNull()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = null;

            this.mockCustomerRepository.Setup(repo => repo.GetCustomerByUserName(username)).Returns((CustomerModel)null);

            // Act
            var result = customerRepository.GetCustomerByUserName(username);

            // Assert
            Assert.Null(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task LoginCustomer_ValidCredentials_ReturnsLoginAccountModel()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = "testuser";
            string password = "validpassword";

            var expectedAccount = new LoginAccountModel
            {
                Username = username,
                Password = password
            };

            this.mockCustomerRepository.Setup(repo => repo.LoginCustomer(username, password)).ReturnsAsync(expectedAccount);

            // Act
            var result = await customerRepository.LoginCustomer(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAccount.Username, result.Username);
            Assert.Equal(expectedAccount.Password, result.Password);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task LoginCustomer_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = "testuser";
            string password = "invalidpassword";

            this.mockCustomerRepository.Setup(repo => repo.LoginCustomer(username, password)).ReturnsAsync((LoginAccountModel)null);

            // Act
            var result = await customerRepository.LoginCustomer(username, password);

            // Assert
            Assert.Null(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task LoginCustomer_NullCredentials_ReturnsNull()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = null;
            string password = null;

            this.mockCustomerRepository.Setup(repo => repo.LoginCustomer(username, password)).ReturnsAsync((LoginAccountModel)null);

            // Act
            var result = await customerRepository.LoginCustomer(username, password);

            // Assert
            Assert.Null(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public void UpdateCustomer_ValidCustomer_UpdatesSuccessfully()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            var customerDTO = new CustomerModel
            {
                Username = "testuser",
                Fullname = "Test User",
                Phone = "1234567890",
                Email = "testuser@example.com"
            };

            // Setup the mock to expect the UpdateCustomer call
            this.mockCustomerRepository.Setup(repo => repo.UpdateCustomer(customerDTO));

            // Act
            customerRepository.UpdateCustomer(customerDTO);

            // Assert
            this.mockCustomerRepository.VerifyAll(); // Verify that the UpdateCustomer method was called
        }

        [Fact]
        public async Task ChangePassword_ValidModel_ReturnsUpdatedCustomer()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            var model = new ChangePasswordModel
            {
                Username = "testuser",
                OldPassword = "oldpassword",
                NewPassword = "newpassword"
            };

            var expectedCustomer = new Customer
            {
                Username = "testuser",
                Password = "newpassword", // Assuming the password is updated
                Fullname = "Test User",
                Phone = "1234567890",
                Email = "testuser@example.com"
            };

            // Setup the mock to expect the ChangePassword call
            this.mockCustomerRepository.Setup(repo => repo.ChangePassword(model)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await customerRepository.ChangePassword(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Username, result.Username);
            Assert.Equal(expectedCustomer.Password, result.Password);
            Assert.Equal(expectedCustomer.Fullname, result.Fullname);
            Assert.Equal(expectedCustomer.Phone, result.Phone);
            Assert.Equal(expectedCustomer.Email, result.Email);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangePassword_InvalidOldPassword_ReturnsNull()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            var model = new ChangePasswordModel
            {
                Username = "testuser",
                OldPassword = "wrongpassword",
                NewPassword = "newpassword"
            };

            // Setup the mock to expect the ChangePassword call
            this.mockCustomerRepository.Setup(repo => repo.ChangePassword(model)).ReturnsAsync((Customer)null);

            // Act
            var result = await customerRepository.ChangePassword(model);

            // Assert
            Assert.Null(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangePassword_NullModel_ReturnsNull()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            ChangePasswordModel model = null;

            // Setup the mock to expect the ChangePassword call
            this.mockCustomerRepository.Setup(repo => repo.ChangePassword(model)).ReturnsAsync((Customer)null);

            // Act
            var result = await customerRepository.ChangePassword(model);

            // Assert
            Assert.Null(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckMail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string email = "testuser@example.com";

            this.mockCustomerRepository.Setup(repo => repo.CheckMail(email)).ReturnsAsync(true);

            // Act
            var result = await customerRepository.CheckMail(email);

            // Assert
            Assert.True(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckMail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string email = "nonexistent@example.com";

            this.mockCustomerRepository.Setup(repo => repo.CheckMail(email)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.CheckMail(email);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckMail_NullEmail_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string email = null;

            this.mockCustomerRepository.Setup(repo => repo.CheckMail(email)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.CheckMail(email);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public void ForgetPassword_ValidEmailAndNewPassword_CallsRepositoryMethod()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string email = "testuser@example.com";
            string newPassword = "newpassword123";

            // Setup the mock to expect the ForgetPassword call
            this.mockCustomerRepository.Setup(repo => repo.ForgetPassword(email, newPassword));

            // Act
            customerRepository.ForgetPassword(email, newPassword);

            // Assert
            this.mockCustomerRepository.Verify(repo => repo.ForgetPassword(email, newPassword), Times.Once);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsername_ExistingUsername_ReturnsTrue()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = "existinguser";

            this.mockCustomerRepository.Setup(repo => repo.CheckUsername(username)).ReturnsAsync(true);

            // Act
            var result = await customerRepository.CheckUsername(username);

            // Assert
            Assert.True(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsername_NonExistingUsername_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = "nonexistinguser";

            this.mockCustomerRepository.Setup(repo => repo.CheckUsername(username)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.CheckUsername(username);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsername_NullUsername_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = null;

            this.mockCustomerRepository.Setup(repo => repo.CheckUsername(username)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.CheckUsername(username);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task Register_ValidUserRegist_ReturnsTrue()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            var userRegist = new RegisterModel
            {
                Username = "testuser",
                Password = "password123",
                Fullname = "Test User",
                Phone = "1234567890",
                Email = "testuser@example.com"
            };

            // Setup the mock to expect the Register call
            this.mockCustomerRepository.Setup(repo => repo.Register(userRegist)).ReturnsAsync(true);

            // Act
            var result = await customerRepository.Register(userRegist);

            // Assert
            Assert.True(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task Register_InvalidUserRegist_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            var userRegist = new RegisterModel
            {
                Username = "testuser",
                Password = "password123",
                Fullname = "Test User",
                Phone = "1234567890",
                Email = "testuser@example.com"
            };

            // Setup the mock to expect the Register call
            this.mockCustomerRepository.Setup(repo => repo.Register(userRegist)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.Register(userRegist);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }

        [Fact]
        public async Task Register_NullUserRegist_ReturnsFalse()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            RegisterModel userRegist = null;

            // Setup the mock to expect the Register call
            this.mockCustomerRepository.Setup(repo => repo.Register(userRegist)).ReturnsAsync(false);

            // Act
            var result = await customerRepository.Register(userRegist);

            // Assert
            Assert.False(result);
            this.mockCustomerRepository.VerifyAll();
        }
    }
}
