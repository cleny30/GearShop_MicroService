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
    public class ManagerRepositoryTests
    {
        private Mock<IManagerRepository> mockManagerRepository;

        public ManagerRepositoryTests()
        {
            this.mockManagerRepository = new Mock<IManagerRepository>(MockBehavior.Strict);
        }

        private IManagerRepository CreateManagerRepository()
        {
            return this.mockManagerRepository.Object;
        }

        [Fact]
        public async Task CheckManagerExistedAsync_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = "manager1";
            string password = "wrongpassword";

            this.mockManagerRepository.Setup(repo => repo.CheckManagerExistedAsync(username, password)).ReturnsAsync(false);

            // Act
            var result = await managerRepository.CheckManagerExistedAsync(username, password);

            // Assert
            Assert.False(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckManagerExistedAsync_NullCredentials_ReturnsFalse()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;
            string password = null;

            this.mockManagerRepository.Setup(repo => repo.CheckManagerExistedAsync(username, password)).ReturnsAsync(false);

            // Act
            var result = await managerRepository.CheckManagerExistedAsync(username, password);

            // Assert
            Assert.False(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsernameExistedAsync_ExistingUsername_ReturnsTrue()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = "existinguser";

            this.mockManagerRepository.Setup(repo => repo.CheckUsernameExistedAsync(username)).ReturnsAsync(true);

            // Act
            var result = await managerRepository.CheckUsernameExistedAsync(username);

            // Assert
            Assert.True(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsernameExistedAsync_NonExistingUsername_ReturnsFalse()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = "nonexistinguser";

            this.mockManagerRepository.Setup(repo => repo.CheckUsernameExistedAsync(username)).ReturnsAsync(false);

            // Act
            var result = await managerRepository.CheckUsernameExistedAsync(username);

            // Assert
            Assert.False(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsernameExistedAsync_NullUsername_ReturnsFalse()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;

            this.mockManagerRepository.Setup(repo => repo.CheckUsernameExistedAsync(username)).ReturnsAsync(false);

            // Act
            var result = await managerRepository.CheckUsernameExistedAsync(username);

            // Assert
            Assert.False(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public void GetAll_ReturnsListOfManagers()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            var expectedManagers = new List<Manager>
        {
            new Manager { Id = 1, Username = "manager1", Password = "password1", Fullname = "Manager One", Phone = "1234567890", SSN = "123-45-6789", Address = "123 Main St", IsAdmin = true },
            new Manager { Id = 2, Username = "manager2", Password = "password2", Fullname = "Manager Two", Phone = "0987654321", SSN = "987-65-4321", Address = "456 Elm St", IsAdmin = false }
        };

            this.mockManagerRepository.Setup(repo => repo.GetAll()).Returns(expectedManagers);

            // Act
            var result = managerRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedManagers.Count, result.Count);
            Assert.Equal(expectedManagers[0].Username, result[0].Username);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task GetManagerByUsername_ValidUsername_ReturnsManagerModel()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = "testuser";
            var expectedManager = new ManagerModel
            {
                Id = 1,
                Username = username,
                Password = "password123",
                Fullname = "Test User",
                Phone = "1234567890",
                SSN = "123-45-6789",
                Address = "123 Main St"
            };

            this.mockManagerRepository.Setup(repo => repo.GetManagerByUsername(username)).ReturnsAsync(expectedManager);

            // Act
            var result = await managerRepository.GetManagerByUsername(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedManager.Username, result.Username);
            Assert.Equal(expectedManager.Fullname, result.Fullname);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task GetManagerByUsername_NonExistingUsername_ReturnsNull()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = "nonexistinguser";

            this.mockManagerRepository.Setup(repo => repo.GetManagerByUsername(username)).ReturnsAsync((ManagerModel)null);

            // Act
            var result = await managerRepository.GetManagerByUsername(username);

            // Assert
            Assert.Null(result);
            this.mockManagerRepository.VerifyAll();
        }

        [Fact]
        public async Task GetManagerByUsername_NullUsername_ReturnsNull()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;

            this.mockManagerRepository.Setup(repo => repo.GetManagerByUsername(username)).ReturnsAsync((ManagerModel)null);

            // Act
            var result = await managerRepository.GetManagerByUsername(username);

            // Assert
            Assert.Null(result);
            this.mockManagerRepository.VerifyAll();
        }
    }
}
