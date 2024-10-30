using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ProductAttributeRepositoryTests
    {
        private Mock<IProductAttributeRepository> mockProductAttributeRepository;



        public ProductAttributeRepositoryTests()
        {
            this.mockProductAttributeRepository = new Mock<IProductAttributeRepository>(MockBehavior.Strict);


        }

        private IProductAttributeRepository CreateProductAttributeRepository()
        {
            return this.mockProductAttributeRepository.Object;
        }

        [Fact]
        public async Task AddProductAttribute_ValidAttributes_ReturnsTrue()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            var productAttributes = new List<ProductAttributeModel>
        {
            new ProductAttributeModel { ProId = "P001", Feature = "Color", Description = "Red" },
            new ProductAttributeModel { ProId = "P001", Feature = "Size", Description = "Medium" }
        };

            // Setup the mock to expect the AddProductAttribute call
            this.mockProductAttributeRepository.Setup(repo => repo.AddProductAttribute(productAttributes)).ReturnsAsync(true);

            // Act
            var result = await productAttributeRepository.AddProductAttribute(productAttributes);

            // Assert
            Assert.True(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProductAttribute_NullAttributes_ReturnsFalse()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            List<ProductAttributeModel> productAttributes = null;

            // Setup the mock to expect the AddProductAttribute call
            this.mockProductAttributeRepository.Setup(repo => repo.AddProductAttribute(productAttributes)).ReturnsAsync(false);

            // Act
            var result = await productAttributeRepository.AddProductAttribute(productAttributes);

            // Assert
            Assert.False(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductAttributeByID_ValidProId_ReturnsTrue()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = "P001"; // Use a valid product ID for testing

            // Setup the mock to expect the DeleteProductAttributeByID call
            this.mockProductAttributeRepository.Setup(repo => repo.DeleteProductAttributeByID(proId)).ReturnsAsync(true);

            // Act
            var result = await productAttributeRepository.DeleteProductAttributeByID(proId);

            // Assert
            Assert.True(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductAttributeByID_InvalidProId_ReturnsFalse()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = "InvalidProId"; // Use an invalid product ID

            // Setup the mock to expect the DeleteProductAttributeByID call
            this.mockProductAttributeRepository.Setup(repo => repo.DeleteProductAttributeByID(proId)).ReturnsAsync(false);

            // Act
            var result = await productAttributeRepository.DeleteProductAttributeByID(proId);

            // Assert
            Assert.False(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductAttributeByID_NullProId_ReturnsFalse()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = null; // Null product ID

            // Setup the mock to expect the DeleteProductAttributeByID call
            this.mockProductAttributeRepository.Setup(repo => repo.DeleteProductAttributeByID(proId)).ReturnsAsync(false);

            // Act
            var result = await productAttributeRepository.DeleteProductAttributeByID(proId);

            // Assert
            Assert.False(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductAttributesByID_ValidProId_ReturnsListOfProductAttributeModels()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = "P001"; // Use a valid product ID for testing
            var expectedAttributes = new List<ProductAttributeModel>
        {
            new ProductAttributeModel { ProId = proId, Feature = "Color", Description = "Red" },
            new ProductAttributeModel { ProId = proId, Feature = "Size", Description = "Medium" }
        };

            // Setup the mock to expect the GetProductAttributesByID call
            this.mockProductAttributeRepository.Setup(repo => repo.GetProductAttributesByID(proId)).ReturnsAsync(expectedAttributes);

            // Act
            var result = await productAttributeRepository.GetProductAttributesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAttributes.Count, result.Count);
            Assert.Equal(expectedAttributes[0].Feature, result[0].Feature);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductAttributesByID_InvalidProId_ReturnsEmptyList()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = "InvalidProId"; // Use an invalid product ID

            // Setup the mock to expect the GetProductAttributesByID call
            this.mockProductAttributeRepository.Setup(repo => repo.GetProductAttributesByID(proId)).ReturnsAsync(new List<ProductAttributeModel>());

            // Act
            var result = await productAttributeRepository.GetProductAttributesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductAttributeRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductAttributesByID_NullProId_ReturnsEmptyList()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string proId = null; // Null product ID

            // Setup the mock to expect the GetProductAttributesByID call
            this.mockProductAttributeRepository.Setup(repo => repo.GetProductAttributesByID(proId)).ReturnsAsync(new List<ProductAttributeModel>());

            // Act
            var result = await productAttributeRepository.GetProductAttributesByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductAttributeRepository.VerifyAll();
        }
    }
}
