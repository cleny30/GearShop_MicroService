using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ProductImageRepositoryTests
    {
        private Mock<IProductImageRepository> mockProductImageRepository;



        public ProductImageRepositoryTests()
        {
            this.mockProductImageRepository = new Mock<IProductImageRepository>(MockBehavior.Strict);


        }

        private IProductImageRepository CreateProductImageRepository()
        {
            return this.mockProductImageRepository.Object;
        }

        [Fact]
        public async Task AddImageOfSpecificProduct_ValidImages_ReturnsTrue()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            var imageLinks = new List<ProductImageModel>
        {
            new ProductImageModel { ProId = "P001", ProImg = "image1.jpg" },
            new ProductImageModel { ProId = "P001", ProImg = "image2.jpg" }
        };

            // Setup the mock to expect the AddImageOfSpecificProduct call
            this.mockProductImageRepository.Setup(repo => repo.AddImageOfSpecificProduct(imageLinks)).ReturnsAsync(true);

            // Act
            var result = await productImageRepository.AddImageOfSpecificProduct(imageLinks);

            // Assert
            Assert.True(result);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task AddImageOfSpecificProduct_NullImages_ReturnsFalse()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            List<ProductImageModel> imageLinks = null;

            // Setup the mock to expect the AddImageOfSpecificProduct call
            this.mockProductImageRepository.Setup(repo => repo.AddImageOfSpecificProduct(imageLinks)).ReturnsAsync(false);

            // Act
            var result = await productImageRepository.AddImageOfSpecificProduct(imageLinks);

            // Assert
            Assert.False(result);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductImagesByID_ValidProId_ReturnsListOfProductImageModels()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            string proId = "P001"; // Use a valid product ID for testing
            var expectedImages = new List<ProductImageModel>
        {
            new ProductImageModel { ProId = proId, ProImg = "image1.jpg" },
            new ProductImageModel { ProId = proId, ProImg = "image2.jpg" }
        };

            // Setup the mock to expect the GetProductImagesByID call
            this.mockProductImageRepository.Setup(repo => repo.GetProductImagesByID(proId)).ReturnsAsync(expectedImages);

            // Act
            var result = await productImageRepository.GetProductImagesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedImages.Count, result.Count);
            Assert.Equal(expectedImages[0].ProImg, result[0].ProImg);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductImagesByID_InvalidProId_ReturnsEmptyList()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            string proId = "InvalidProId"; // Use an invalid product ID

            // Setup the mock to expect the GetProductImagesByID call
            this.mockProductImageRepository.Setup(repo => repo.GetProductImagesByID(proId)).ReturnsAsync(new List<ProductImageModel>());

            // Act
            var result = await productImageRepository.GetProductImagesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductImagesByID_NullProId_ReturnsEmptyList()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            string proId = null; // Null product ID

            // Setup the mock to expect the GetProductImagesByID call
            this.mockProductImageRepository.Setup(repo => repo.GetProductImagesByID(proId)).ReturnsAsync(new List<ProductImageModel>());

            // Act
            var result = await productImageRepository.GetProductImagesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task RemoveImageByID_ValidImageLinks_ReturnsTrue()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            var imageLinks = new List<ProductImageModel>
        {
            new ProductImageModel { ProId = "P001", ProImg = "image1.jpg" },
            new ProductImageModel { ProId = "P002", ProImg = "image2.jpg" }
        };

            // Setup the mock to expect the RemoveImageByID call
            this.mockProductImageRepository.Setup(repo => repo.RemoveImageByID(imageLinks)).ReturnsAsync(true);

            // Act
            var result = await productImageRepository.RemoveImageByID(imageLinks);

            // Assert
            Assert.True(result);
            this.mockProductImageRepository.VerifyAll();
        }

        [Fact]
        public async Task RemoveImageByID_NullImageLinks_ReturnsFalse()
        {
            // Arrange
            var productImageRepository = this.CreateProductImageRepository();
            List<ProductImageModel> imageLinks = null;

            // Setup the mock to expect the RemoveImageByID call
            this.mockProductImageRepository.Setup(repo => repo.RemoveImageByID(imageLinks)).ReturnsAsync(false);

            // Act
            var result = await productImageRepository.RemoveImageByID(imageLinks);

            // Assert
            Assert.False(result);
            this.mockProductImageRepository.VerifyAll();
        }
    }
}
