using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class ImportProductRepositoryTests
    {
        private MockRepository mockRepository;



        public ImportProductRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private ImportProductRepository CreateImportProductRepository()
        {
            return new ImportProductRepository();
        }

        [Fact]
        public async Task AddReceiptProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            List<ReceiptProductModel> list = null;

            // Act
            var result = await importProductRepository.AddReceiptProduct(
                list);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateImportReceipt_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            ImportProductModel _ImportProduct = null;

            // Act
            var result = await importProductRepository.CreateImportReceipt(
                _ImportProduct);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetImportProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int ImportProductId = 0;

            // Act
            var result = await importProductRepository.GetImportProduct(
                ImportProductId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetImportProductsList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();

            // Act
            var result = await importProductRepository.GetImportProductsList();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetMoneySpent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();

            // Act
            var result = await importProductRepository.GetMoneySpent();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetReceiptProductsByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var importProductRepository = this.CreateImportProductRepository();
            int ImportProductId = 0;

            // Act
            var result = await importProductRepository.GetReceiptProductsByID(
                ImportProductId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
