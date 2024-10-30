using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ProductAttributeRepositoryTests
    {
        private MockRepository mockRepository;



        public ProductAttributeRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ProductAttributeRepository CreateProductAttributeRepository()
        {
            return new ProductAttributeRepository();
        }

        [Fact]
        public async Task AddProductAttribute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            List<ProductAttributeModel> productAttributes = null;

            // Act
            var result = await productAttributeRepository.AddProductAttribute(
                productAttributes);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductAttributeByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string ProId = null;

            // Act
            var result = await productAttributeRepository.DeleteProductAttributeByID(
                ProId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductAttributesByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productAttributeRepository = this.CreateProductAttributeRepository();
            string ProId = null;

            // Act
            var result = await productAttributeRepository.GetProductAttributesByID(
                ProId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
