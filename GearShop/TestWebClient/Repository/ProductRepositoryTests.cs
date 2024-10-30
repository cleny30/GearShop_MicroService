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
    public class ProductRepositoryTests
    {
        private Mock<IProductRepository> mockProductRepository;



        public ProductRepositoryTests()
        {
            this.mockProductRepository = new Mock<IProductRepository>(MockBehavior.Strict);
        }

        private IProductRepository CreateProductRepository()
        {
            return this.mockProductRepository.Object;
        }

        [Fact]
        public void GetProductAttributes_ReturnsListOfProductAttributes()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedAttributes = new List<ProductAttribute>
        {
            new ProductAttribute { ProId = "P001", Feature = "Color", Description = "Red" },
            new ProductAttribute { ProId = "P002", Feature = "Size", Description = "Medium" }
        };

            // Setup the mock to expect the GetProductAttributes call
            this.mockProductRepository.Setup(repo => repo.GetProductAttributes()).Returns(expectedAttributes);

            // Act
            var result = productRepository.GetProductAttributes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAttributes.Count, result.Count);
            Assert.Equal(expectedAttributes[0].Feature, result[0].Feature);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetProductAttributes_ReturnsEmptyList_WhenNoAttributes()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedAttributes = new List<ProductAttribute>(); // Empty list

            // Setup the mock to expect the GetProductAttributes call
            this.mockProductRepository.Setup(repo => repo.GetProductAttributes()).Returns(expectedAttributes);

            // Act
            var result = productRepository.GetProductAttributes();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetProductImages_ReturnsListOfProductImages()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedImages = new List<ProductImage>
        {
            new ProductImage { ProId = "P001", ProImg = "image1.jpg" },
            new ProductImage { ProId = "P002", ProImg = "image2.jpg" }
        };

            // Setup the mock to expect the GetProductImages call
            this.mockProductRepository.Setup(repo => repo.GetProductImages()).Returns(expectedImages);

            // Act
            var result = productRepository.GetProductImages();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedImages.Count, result.Count);
            Assert.Equal(expectedImages[0].ProImg, result[0].ProImg);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetProductImages_ReturnsEmptyList_WhenNoImages()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedImages = new List<ProductImage>(); // Empty list

            // Setup the mock to expect the GetProductImages call
            this.mockProductRepository.Setup(repo => repo.GetProductImages()).Returns(expectedImages);

            // Act
            var result = productRepository.GetProductImages();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedProducts = new List<Product>
        {
            new Product { ProId = "P001", CateId = 1, BrandId = 1, ProName = "Product 1", ProQuan = 100, ProPrice = 19.99, ProDes = "Description 1", Discount = 10, IsAvailable = true },
            new Product { ProId = "P002", CateId = 1, BrandId = 2, ProName = "Product 2", ProQuan = 50, ProPrice = 29.99, ProDes = "Description 2", Discount = 5, IsAvailable = true }
        };

            // Setup the mock to expect the GetProducts call
            this.mockProductRepository.Setup(repo => repo.GetProducts()).Returns(expectedProducts);

            // Act
            var result = productRepository.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts[0].ProName, result[0].ProName);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public void GetProducts_ReturnsEmptyList_WhenNoProducts()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedProducts = new List<Product>(); // Empty list

            // Setup the mock to expect the GetProducts call
            this.mockProductRepository.Setup(repo => repo.GetProducts()).Returns(expectedProducts);

            // Act
            var result = productRepository.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductListAdmin_ReturnsListOfProductModels()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedProducts = new List<ProductModel>
        {
            new ProductModel { ProId = "P001", ProName = "Gaming Mouse", ProPrice = 49.99, ProQuan = 100, Discount = 10, IsAvailable = true },
            new ProductModel { ProId = "P002", ProName = "Mechanical Keyboard", ProPrice = 79.99, ProQuan = 50, Discount = 5, IsAvailable = true }
        };

            // Setup the mock to expect the GetProductListAdmin call
            this.mockProductRepository.Setup(repo => repo.GetProductListAdmin()).ReturnsAsync(expectedProducts);

            // Act
            var result = await productRepository.GetProductListAdmin();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts[0].ProName, result[0].ProName);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductListAdmin_ReturnsEmptyList_WhenNoProducts()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var expectedProducts = new List<ProductModel>(); // Empty list

            // Setup the mock to expect the GetProductListAdmin call
            this.mockProductRepository.Setup(repo => repo.GetProductListAdmin()).ReturnsAsync(expectedProducts);

            // Act
            var result = await productRepository.GetProductListAdmin();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task SearchProductsByName_ValidProductName_ReturnsListOfProductData()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string productName = "Gaming Mouse"; // Use a valid product name for testing
            var expectedProducts = new List<ProductData>
        {
            new ProductData { ProId = "P001", CateId = 1, BrandId = 1, ProName = "Gaming Mouse", ProQuan = 100, ProPrice = 49.99, ProDes = "High precision gaming mouse", Discount = 10, IsAvailable = true },
            new ProductData { ProId = "P002", CateId = 1, BrandId = 2, ProName = "Wireless Gaming Mouse", ProQuan = 50, ProPrice = 59.99, ProDes = "Wireless mouse for gaming", Discount = 5, IsAvailable = true }
        };

            // Setup the mock to expect the SearchProductsByName call
            this.mockProductRepository.Setup(repo => repo.SearchProductsByName(productName)).ReturnsAsync(expectedProducts);

            // Act
            var result = await productRepository.SearchProductsByName(productName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts[0].ProName, result[0].ProName);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task SearchProductsByName_NonExistingProductName_ReturnsEmptyList()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string productName = "NonExistingProduct"; // Use a non-existing product name

            // Setup the mock to expect the SearchProductsByName call
            this.mockProductRepository.Setup(repo => repo.SearchProductsByName(productName)).ReturnsAsync(new List<ProductData>()); // Empty list

            // Act
            var result = await productRepository.SearchProductsByName(productName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task SearchProductsByName_NullProductName_ReturnsEmptyList()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string productName = null; // Null product name

            // Setup the mock to expect the SearchProductsByName call
            this.mockProductRepository.Setup(repo => repo.SearchProductsByName(productName)).ReturnsAsync(new List<ProductData>()); // Empty list

            // Act
            var result = await productRepository.SearchProductsByName(productName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewProductID_ValidCatID_ReturnsNewProductID()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int catId = 1; // Use a valid category ID for testing
            string expectedProductId = "CT005"; // Example expected product ID

            // Setup the mock to expect the GetNewProductID call
            this.mockProductRepository.Setup(repo => repo.GetNewProductID(catId)).ReturnsAsync(expectedProductId);

            // Act
            var result = await productRepository.GetNewProductID(catId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProductId, result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetNewProductID_InvalidCatID_ReturnsNull()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int catId = 0; // Use an invalid category ID

            // Setup the mock to expect the GetNewProductID call
            this.mockProductRepository.Setup(repo => repo.GetNewProductID(catId)).ReturnsAsync((string)null);

            // Act
            var result = await productRepository.GetNewProductID(catId);

            // Assert
            Assert.Null(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertProduct_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var product = new ProductData
            {
                ProId = "P001",
                CateId = 1,
                BrandId = 1,
                ProName = "Gaming Mouse",
                ProQuan = 100,
                ProPrice = 49.99,
                ProDes = "High precision gaming mouse",
                Discount = 10,
                IsAvailable = true
            };

            // Setup the mock to expect the InsertProduct call
            this.mockProductRepository.Setup(repo => repo.InsertProduct(product)).ReturnsAsync(true);

            // Act
            var result = await productRepository.InsertProduct(product);

            // Assert
            Assert.True(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertProduct_NullProduct_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            ProductData product = null;

            // Setup the mock to expect the InsertProduct call
            this.mockProductRepository.Setup(repo => repo.InsertProduct(product)).ReturnsAsync(false);

            // Act
            var result = await productRepository.InsertProduct(product);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductByID_ValidProId_ReturnsProductModel()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = "P001"; // Use a valid product ID for testing
            var expectedProduct = new ProductModel
            {
                ProId = proId,
                ProName = "Gaming Mouse",
                ProPrice = 49.99,
                ProQuan = 100,
                Discount = 10,
                IsAvailable = true,
                CateId = 1,
                BrandId = 1,
                CateName = "Electronics",
                BrandName = "Brand A",
                ProDes = "High precision gaming mouse"
            };

            // Setup the mock to expect the GetProductByID call
            this.mockProductRepository.Setup(repo => repo.GetProductByID(proId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await productRepository.GetProductByID(proId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.ProId, result.ProId);
            Assert.Equal(expectedProduct.ProName, result.ProName);
            Assert.Equal(expectedProduct.ProPrice, result.ProPrice);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductByID_InvalidProId_ReturnsNull()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = "InvalidProId"; // Use an invalid product ID

            // Setup the mock to expect the GetProductByID call
            this.mockProductRepository.Setup(repo => repo.GetProductByID(proId)).ReturnsAsync((ProductModel)null);

            // Act
            var result = await productRepository.GetProductByID(proId);

            // Assert
            Assert.Null(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductByID_NullProId_ReturnsNull()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = null; // Null product ID

            // Setup the mock to expect the GetProductByID call
            this.mockProductRepository.Setup(repo => repo.GetProductByID(proId)).ReturnsAsync((ProductModel)null);

            // Act
            var result = await productRepository.GetProductByID(proId);

            // Assert
            Assert.Null(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var product = new ProductData
            {
                ProId = "P001",
                CateId = 1,
                BrandId = 1,
                ProName = "Gaming Mouse",
                ProQuan = 100,
                ProPrice = 49.99,
                ProDes = "High precision gaming mouse",
                Discount = 10,
                IsAvailable = true
            };

            // Setup the mock to expect the UpdateProduct call
            this.mockProductRepository.Setup(repo => repo.UpdateProduct(product)).ReturnsAsync(true);

            // Act
            var result = await productRepository.UpdateProduct(product);

            // Assert
            Assert.True(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_NullProduct_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            ProductData product = null;

            // Setup the mock to expect the UpdateProduct call
            this.mockProductRepository.Setup(repo => repo.UpdateProduct(product)).ReturnsAsync(false);

            // Act
            var result = await productRepository.UpdateProduct(product);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeProductStatus_ValidProIdAndStatus_ReturnsTrue()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = "P001"; // Use a valid product ID for testing
            bool status = true; // Example status

            // Setup the mock to expect the ChangeProductStatus call
            this.mockProductRepository.Setup(repo => repo.ChangeProductStatus(proId, status)).ReturnsAsync(true);

            // Act
            var result = await productRepository.ChangeProductStatus(proId, status);

            // Assert
            Assert.True(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeProductStatus_InvalidProId_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = "InvalidProId"; // Use an invalid product ID
            bool status = false; // Example status

            // Setup the mock to expect the ChangeProductStatus call
            this.mockProductRepository.Setup(repo => repo.ChangeProductStatus(proId, status)).ReturnsAsync(false);

            // Act
            var result = await productRepository.ChangeProductStatus(proId, status);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeProductStatus_NullProId_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            string proId = null; // Null product ID
            bool status = false; // Example status

            // Setup the mock to expect the ChangeProductStatus call
            this.mockProductRepository.Setup(repo => repo.ChangeProductStatus(proId, status)).ReturnsAsync(false);

            // Act
            var result = await productRepository.ChangeProductStatus(proId, status);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddQuantityToProduct_ValidProducts_ReturnsTrue()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var products = new List<ReceiptProductModel>
        {
            new ReceiptProductModel { ReceiptId = 1, ImportProductId = 101, ProId = "P001", ProName = "Product 1", Amount = 10, Price = 15.99 },
            new ReceiptProductModel { ReceiptId = 2, ImportProductId = 102, ProId = "P002", ProName = "Product 2", Amount = 5, Price = 25.50 }
        };

            // Setup the mock to expect the AddQuantityToProduct call
            this.mockProductRepository.Setup(repo => repo.AddQuantityToProduct(products)).ReturnsAsync(true);

            // Act
            var result = await productRepository.AddQuantityToProduct(products);

            // Assert
            Assert.True(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddQuantityToProduct_NullProducts_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            List<ReceiptProductModel> products = null;

            // Setup the mock to expect the AddQuantityToProduct call
            this.mockProductRepository.Setup(repo => repo.AddQuantityToProduct(products)).ReturnsAsync(false);

            // Act
            var result = await productRepository.AddQuantityToProduct(products);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateQuantityToProduct_ValidProducts_ReturnsTrue()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            var products = new List<ProductData>
        {
            new ProductData { ProId = "P001", CateId = 1, BrandId = 1, ProName = "Product 1", ProQuan = 10, ProPrice = 19.99, ProDes = "Description 1", Discount = 10, IsAvailable = true },
            new ProductData { ProId = "P002", CateId = 1, BrandId = 2, ProName = "Product 2", ProQuan = 5, ProPrice = 29.99, ProDes = "Description 2", Discount = 5, IsAvailable = true }
        };

            // Setup the mock to expect the UpdateQuantityToProduct call
            this.mockProductRepository.Setup(repo => repo.UpdateQuantityToProduct(products)).ReturnsAsync(true);

            // Act
            var result = await productRepository.UpdateQuantityToProduct(products);

            // Assert
            Assert.True(result);
            this.mockProductRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateQuantityToProduct_NullProducts_ReturnsFalse()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            List<ProductData> products = null;

            // Setup the mock to expect the UpdateQuantityToProduct call
            this.mockProductRepository.Setup(repo => repo.UpdateQuantityToProduct(products)).ReturnsAsync(false);

            // Act
            var result = await productRepository.UpdateQuantityToProduct(products);

            // Assert
            Assert.False(result);
            this.mockProductRepository.VerifyAll();
        }
    }
}
