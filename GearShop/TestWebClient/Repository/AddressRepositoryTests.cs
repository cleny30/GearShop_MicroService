using BusinessObject.Model.Entity;
using Moq;
using Repository.Repository;
using System;
using Xunit;

namespace TestWebClient.Repository
{
    public class AddressRepositoryTests
    {
        private MockRepository mockRepository;



        public AddressRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private AddressRepository CreateAddressRepository()
        {
            return new AddressRepository();
        }

        [Fact]
        public void GetAllAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            string username = null;

            // Act
            var result = addressRepository.GetAllAddress(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void AddNewAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            DeliveryAddressModel deliveryAddressModel = null;
            string username = null;

            // Act
            var result = addressRepository.AddNewAddress(
                deliveryAddressModel,
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void UpdateAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            DeliveryAddressModel deliveryAddressModel = null;

            // Act
            var result = addressRepository.UpdateAddress(
                deliveryAddressModel);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void DeleteAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            int id = 0;

            // Act
            var result = addressRepository.DeleteAddress(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
