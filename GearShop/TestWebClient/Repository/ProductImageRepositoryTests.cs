using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ProductImageRepositoryTests
    {
        private MockRepository mockRepository;



        public ProductImageRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ProductImageRepository CreateProductImageRepository()
        {
            return new ProductImageRepository();
        }

        [Fact]
        public async Task AddImageOfSpecificProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            List<ProductImageModel> imageLink = null;

            // Act
            var result = await productImageRepository.AddImageOfSpecificProduct(
                imageLink);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductImagesByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            string ProId = null;

            // Act
            var result = await productImageRepository.GetProductImagesByID(
                ProId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task RemoveImageByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            List<ProductImageModel> imageLink = null;

            // Act
            var result = await productImageRepository.RemoveImageByID(
                imageLink);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
