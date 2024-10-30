using BusinessObject.DTOS;
using Moq;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ImportProductRepositoryTests
    {
        private Mock<IImportProductRepository> mockImportProductRepository;



        public ImportProductRepositoryTests()
        {
            this.mockImportProductRepository = new Mock<IImportProductRepository>(MockBehavior.Strict);
        }
        private IImportProductRepository CreateImportProductRepository()
        {
            return this.mockImportProductRepository.Object;
        }

        [Fact]
        public async Task AddReceiptProduct_ValidList_ReturnsTrue()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            var list = new List<ReceiptProductModel>
        {
            new ReceiptProductModel { ReceiptId = 1, ImportProductId = 101, ProId = "P001", ProName = "Product 1", Amount = 10, Price = 15.99 },
            new ReceiptProductModel { ReceiptId = 1, ImportProductId = 102, ProId = "P002", ProName = "Product 2", Amount = 5, Price = 25.50 }
        };

            this.mockImportProductRepository.Setup(repo => repo.AddReceiptProduct(list)).ReturnsAsync(true);

            // Act
            var result = await importProductRepository.AddReceiptProduct(list);

            // Assert
            Assert.True(result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddReceiptProduct_EmptyList_ReturnsFalse()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            List<ReceiptProductModel> list = new List<ReceiptProductModel>(); // Empty list

            this.mockImportProductRepository.Setup(repo => repo.AddReceiptProduct(list)).ReturnsAsync(false);

            // Act
            var result = await importProductRepository.AddReceiptProduct(list);

            // Assert
            Assert.False(result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task AddReceiptProduct_NullList_ReturnsFalse()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            List<ReceiptProductModel> list = null;

            this.mockImportProductRepository.Setup(repo => repo.AddReceiptProduct(list)).ReturnsAsync(false);

            // Act
            var result = await importProductRepository.AddReceiptProduct(list);

            // Assert
            Assert.False(result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateImportReceipt_ValidImportProduct_ReturnsImportProductModel()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            var importProduct = new ImportProductModel
            {
                ImportProductId = 1,
                DateImport = DateOnly.FromDateTime(DateTime.Now),
                PersonInCharge = "John Doe",
                Payment = 100.00
            };

            // Setup the mock to expect the CreateImportReceipt call
            this.mockImportProductRepository.Setup(repo => repo.CreateImportReceipt(importProduct)).ReturnsAsync(importProduct);

            // Act
            var result = await importProductRepository.CreateImportReceipt(importProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(importProduct.ImportProductId, result.ImportProductId);
            Assert.Equal(importProduct.PersonInCharge, result.PersonInCharge);
            Assert.Equal(importProduct.Payment, result.Payment);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateImportReceipt_NullImportProduct_ReturnsNull()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            ImportProductModel importProduct = null;

            // Setup the mock to expect the CreateImportReceipt call
            this.mockImportProductRepository.Setup(repo => repo.CreateImportReceipt(importProduct)).ReturnsAsync((ImportProductModel)null);

            // Act
            var result = await importProductRepository.CreateImportReceipt(importProduct);

            // Assert
            Assert.Null(result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetImportProduct_ValidId_ReturnsImportProductModel()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int importProductId = 1; // Use a valid ID for testing
            var expectedImportProduct = new ImportProductModel
            {
                ImportProductId = importProductId,
                DateImport = DateOnly.FromDateTime(DateTime.Now),
                PersonInCharge = "John Doe",
                Payment = 100.00
            };

            // Setup the mock to expect the GetImportProduct call
            this.mockImportProductRepository.Setup(repo => repo.GetImportProduct(importProductId)).ReturnsAsync(expectedImportProduct);

            // Act
            var result = await importProductRepository.GetImportProduct(importProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedImportProduct.ImportProductId, result.ImportProductId);
            Assert.Equal(expectedImportProduct.PersonInCharge, result.PersonInCharge);
            Assert.Equal(expectedImportProduct.Payment, result.Payment);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetImportProduct_InvalidId_ReturnsNull()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int importProductId = 0; // Assuming 0 is an invalid ID

            // Setup the mock to expect the GetImportProduct call
            this.mockImportProductRepository.Setup(repo => repo.GetImportProduct(importProductId)).ReturnsAsync((ImportProductModel)null);

            // Act
            var result = await importProductRepository.GetImportProduct(importProductId);

            // Assert
            Assert.Null(result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetImportProductsList_ReturnsListOfImportProductModels()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            var expectedImportProducts = new List<ImportProductModel>
        {
            new ImportProductModel { ImportProductId = 1, DateImport = DateOnly.FromDateTime(DateTime.Now), PersonInCharge = "John Doe", Payment = 100.00 },
            new ImportProductModel { ImportProductId = 2, DateImport = DateOnly.FromDateTime(DateTime.Now), PersonInCharge = "Jane Smith", Payment = 200.00 }
        };

            this.mockImportProductRepository.Setup(repo => repo.GetImportProductsList()).ReturnsAsync(expectedImportProducts);

            // Act
            var result = await importProductRepository.GetImportProductsList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedImportProducts.Count, result.Count);
            Assert.Equal(expectedImportProducts[0].ImportProductId, result[0].ImportProductId);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetMoneySpent_ReturnsTotalMoneySpent()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            double expectedTotal = 1500.00; // Example total money spent

            // Setup the mock to expect the GetMoneySpent call
            this.mockImportProductRepository.Setup(repo => repo.GetMoneySpent()).ReturnsAsync(expectedTotal);

            // Act
            var result = await importProductRepository.GetMoneySpent();

            // Assert
            Assert.Equal(expectedTotal, result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetMoneySpent_NoMoneySpent_ReturnsZero()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            double expectedTotal = 0.00; // Example total money spent

            // Setup the mock to expect the GetMoneySpent call
            this.mockImportProductRepository.Setup(repo => repo.GetMoneySpent()).ReturnsAsync(expectedTotal);

            // Act
            var result = await importProductRepository.GetMoneySpent();

            // Assert
            Assert.Equal(expectedTotal, result);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetReceiptProductsByID_ValidId_ReturnsListOfReceiptProductModels()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int importProductId = 1; // Use a valid ID for testing
            var expectedReceiptProducts = new List<ReceiptProductModel>
        {
            new ReceiptProductModel { ReceiptId = 1, ImportProductId = importProductId, ProId = "P001", ProName = "Product 1", Amount = 10, Price = 15.99 },
            new ReceiptProductModel { ReceiptId = 2, ImportProductId = importProductId, ProId = "P002", ProName = "Product 2", Amount = 5, Price = 25.50 }
        };

            // Setup the mock to expect the GetReceiptProductsByID call
            this.mockImportProductRepository.Setup(repo => repo.GetReceiptProductsByID(importProductId)).ReturnsAsync(expectedReceiptProducts);

            // Act
            var result = await importProductRepository.GetReceiptProductsByID(importProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReceiptProducts.Count, result.Count);
            Assert.Equal(expectedReceiptProducts[0].ProName, result[0].ProName);
            this.mockImportProductRepository.VerifyAll();
        }

        [Fact]
        public async Task GetReceiptProductsByID_InvalidId_ReturnsEmptyList()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int importProductId = 0; // Assuming 0 is an invalid ID

            // Setup the mock to expect the GetReceiptProductsByID call
            this.mockImportProductRepository.Setup(repo => repo.GetReceiptProductsByID(importProductId)).ReturnsAsync(new List<ReceiptProductModel>());

            // Act
            var result = await importProductRepository.GetReceiptProductsByID(importProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            this.mockImportProductRepository.VerifyAll();
        }
    }
}
