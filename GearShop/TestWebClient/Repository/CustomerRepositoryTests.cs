using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class CustomerRepositoryTests
    {
        private MockRepository mockRepository;



        public CustomerRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private CustomerRepository CreateCustomerRepository()
        {
            return new CustomerRepository();
        }

        [Fact]
        public void GetCustomerByUserName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = null;

            // Act
            var result = customerRepository.GetCustomerByUserName(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task LoginCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string userLogin = null;
            string pass = null;

            // Act
            var result = await customerRepository.LoginCustomer(
                userLogin,
                pass);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void UpdateCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            CustomerModel customerDTO = null;

            // Act
            customerRepository.UpdateCustomer(
                customerDTO);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangePassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            ChangePasswordModel model = null;

            // Act
            var result = await customerRepository.ChangePassword(
                model);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckMail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string mail = null;

            // Act
            var result = await customerRepository.CheckMail(
                mail);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void ForgetPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string mail = null;
            string pass = null;

            // Act
            customerRepository.ForgetPassword(
                mail,
                pass);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsername_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            string username = null;

            // Act
            var result = await customerRepository.CheckUsername(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Register_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            RegisterModel userRegist = null;

            // Act
            var result = await customerRepository.Register(
                userRegist);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
