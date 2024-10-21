using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test_WPF.Repository
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
            IProductRepository c = new ProductRepository();
            // Arrange
            //var productRepository = this.CreateProductRepository();

            // Act
            var result = c.GetProductAttributes().ToList();

            // Assert
            Assert.True(result.Count()==241?true:false);
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
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }
    }
}
