using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ManagerRepositoryTests
    {
        private MockRepository mockRepository;



        public ManagerRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ManagerRepository CreateManagerRepository()
        {
            return new ManagerRepository();
        }

        [Fact]
        public async Task CheckManagerExistedAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;
            string password = null;

            // Act
            var result = await managerRepository.CheckManagerExistedAsync(
                username,
                password);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CheckUsernameExistedAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;

            // Act
            var result = await managerRepository.CheckUsernameExistedAsync(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();

            // Act
            var result = managerRepository.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetManagerByUsername_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerRepository = this.CreateManagerRepository();
            string username = null;

            // Act
            var result = await managerRepository.GetManagerByUsername(
                username);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
