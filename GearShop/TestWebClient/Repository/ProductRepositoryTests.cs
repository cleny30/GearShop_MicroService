using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ProductRepositoryTests
    {
        private MockRepository mockRepository;



        public ProductRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ProductRepository CreateProductRepository()
        {
            return new ProductRepository();
        }

        [Fact]
        public void GetProductAttributes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();

            // Act
            var result = productRepository.GetProductAttributes();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetProductImages_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();

            // Act
            var result = productRepository.GetProductImages();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetProducts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();

            // Act
            var result = productRepository.GetProducts();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductListAdmin_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();

            // Act
            var result = await productRepository.GetProductListAdmin();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SearchProductsByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string productName = null;

            // Act
            var result = await productRepository.SearchProductsByName(
                productName);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewProductID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int CatID = 0;

            // Act
            var result = await productRepository.GetNewProductID(
                CatID);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            ProductData product = null;

            // Act
            var result = await productRepository.InsertProduct(
                product);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string ProId = null;

            // Act
            var result = await productRepository.GetProductByID(
                ProId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            ProductData product = null;

            // Act
            var result = await productRepository.UpdateProduct(
                product);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeProductStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string ProId = null;
            bool Status = false;

            // Act
            var result = await productRepository.ChangeProductStatus(
                ProId,
                Status);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddQuantityToProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            List<ReceiptProductModel> products = null;

            // Act
            var result = await productRepository.AddQuantityToProduct(
                products);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateQuantityToProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            List<ProductData> products = null;

            // Act
            var result = await productRepository.UpdateQuantityToProduct(
                products);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }
    }
}
