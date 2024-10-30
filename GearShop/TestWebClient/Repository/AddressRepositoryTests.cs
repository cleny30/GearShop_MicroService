using BusinessObject.Model.Entity;
using Moq;
using Repository.IRepository;

namespace TestWebClient.Repository
{
    public class AddressRepositoryTests
    {
        private Mock<IAddressRepository> mockAddressRepository;

        public AddressRepositoryTests()
        {
            this.mockAddressRepository = new Mock<IAddressRepository>(MockBehavior.Strict);
        }

        private IAddressRepository CreateAddressRepository()
        {
            return this.mockAddressRepository.Object;
        }

        [Fact]
        public void GetAllAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            string username = "cleny30";
            var expectedAddresses = new List<DeliveryAddressModel>
            {
                new DeliveryAddressModel { Id = 1, Username = "cleny30", Fullname = "Nguyen Chi Hau", Phone = "0123345678", Address = "Xã Phong Thạnh Tây B, Huyện Phước Long, Tỉnh Bạc Liêu", Specific = "11/12, đường 3/2", IsDefault = false },
                new DeliveryAddressModel { Id = 2, Username = "cleny30", Fullname = "Nguyen Anh Khoi", Phone = "0123345678", Address = "Xã Ninh Hòa, Huyện Hồng Dân, Tỉnh Bạc Liêu", Specific = "53", IsDefault = false },
                new DeliveryAddressModel { Id = 3, Username = "cleny30", Fullname = "Le Duc An", Phone = "0123456789", Address = "Xã Việt Lập, Huyện Tân Yên, Tỉnh Bắc Giang", Specific = "53", IsDefault = true }
            };

            this.mockAddressRepository.Setup(repo => repo.GetAllAddress(username)).Returns(expectedAddresses);

            // Act
            var result = addressRepository.GetAllAddress(username);

            // Assert
            Assert.Equal(expectedAddresses.Count, result.Count);
            Assert.Equal(expectedAddresses.First().Username, result.First().Username);
            this.mockAddressRepository.VerifyAll();
        }

        [Fact]
        public void AddNewAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            var deliveryAddressModel = new DeliveryAddressModel
            {
                Fullname = "John Doe",
                Phone = "1234567890",
                Address = "123 Main St",
                Specific = "100",
                IsDefault = true
            };

            string username = "testuser";
            this.mockAddressRepository.Setup(repo => repo.AddNewAddress(deliveryAddressModel,username)).Returns(true);

            // Act
            var result = addressRepository.AddNewAddress(
                deliveryAddressModel,
                username);

            // Assert
            Assert.True(result);
            this.mockAddressRepository.VerifyAll();
        }

        [Fact]
        public void UpdateAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            var deliveryAddressModel = new DeliveryAddressModel
            {
                Id = 4,
                Username = "testuser",
                Fullname = "John Doe V2",
                Phone = "1234567890",
                Address = "123 Main St",
                Specific = "500",
                IsDefault = true
            };
            this.mockAddressRepository.Setup(repo => repo.UpdateAddress(deliveryAddressModel)).Returns(true);
            // Act
            var result = addressRepository.UpdateAddress(
                deliveryAddressModel);

            // Assert
            Assert.True(result);
            this.mockAddressRepository.VerifyAll();
        }

        [Fact]
        public void DeleteAddress_ValidId_ReturnsTrue()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            int id = 4;

            this.mockAddressRepository.Setup(repo => repo.DeleteAddress(id)).Returns(true);

            // Act
            var result = addressRepository.DeleteAddress(id);

            // Assert
            Assert.True(result);
            this.mockAddressRepository.VerifyAll();
        }

        [Fact]
        public void DeleteAddress_InvalidId_ReturnsFalse()
        {
            // Arrange
            var addressRepository = this.CreateAddressRepository();
            int invalidId = 0;

            this.mockAddressRepository.Setup(repo => repo.DeleteAddress(invalidId)).Returns(false);

            // Act
            var result = addressRepository.DeleteAddress(invalidId);

            // Assert
            Assert.False(result);
            this.mockAddressRepository.VerifyAll();
        }
    }
}
