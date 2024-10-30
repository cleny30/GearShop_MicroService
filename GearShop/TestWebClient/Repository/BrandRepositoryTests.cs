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
    public class BrandRepositoryTests
    {
        private Mock<IBrandRepository> mockRepository;

        public BrandRepositoryTests()
        {
            this.mockRepository = new Mock<IBrandRepository>(MockBehavior.Strict);


        }

        private IBrandRepository CreateBrandRepository()
        {
            return this.mockRepository.Object;
        }

        [Fact]
        public void GetBrands_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();

            var expectedBrands = new List<Brand>
        {
            new Brand { BrandId = 1, BrandName = "Brand A", BrandLogo = "LogoA", IsAvailable = true },
            new Brand { BrandId = 2, BrandName = "Brand B", BrandLogo = "LogoB", IsAvailable = false }
        };

            this.mockRepository.Setup(repo => repo.GetBrands()).Returns(expectedBrands);

            // Act
            var result = brandRepository.GetBrands();

            // Assert
            Assert.Equal(expectedBrands.Count, result.Count);
            Assert.Equal(expectedBrands.First().BrandName, result.First().BrandName);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetBrandList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            var expectedBrands = new List<BrandModel>
        {
            new BrandModel { BrandId = 1, BrandName = "Brand A", BrandLogo = "LogoA", quantity = 100, IsAvailable = true, FunctionContent = new object() },
            new BrandModel { BrandId = 2, BrandName = "Brand B", BrandLogo = "LogoB", quantity = 200, IsAvailable = false, FunctionContent = new object() }
        };

            this.mockRepository.Setup(repo => repo.GetBrandList()).ReturnsAsync(expectedBrands);

            // Act
            var result = await brandRepository.GetBrandList();

            // Assert
            Assert.Equal(expectedBrands.Count, result.Count);
            Assert.Equal(expectedBrands.First().BrandName, result.First().BrandName);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeBrandStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            int BrandId = 1;
            bool Status = false;

            this.mockRepository.Setup(repo => repo.ChangeBrandStatus(BrandId, Status)).ReturnsAsync(true);
            // Act
            var result = await brandRepository.ChangeBrandStatus(
                BrandId,
                Status);

            // Assert
            Assert.True(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeBrandStatus_InvalidId_ReturnsFalse()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            int invalidBrandId = 0;
            bool status = true;

            this.mockRepository.Setup(repo => repo.ChangeBrandStatus(invalidBrandId, status)).ReturnsAsync(false);

            // Act
            var result = await brandRepository.ChangeBrandStatus(invalidBrandId, status);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewBrand_ValidBrand_ReturnsTrue()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            var brand = new InsertBrandModel
            {
                BrandId = 1, // Assuming you have a specific ID for testing
                BrandName = "New Brand",
                BrandLogo = "LogoPath",
                IsAvailable = true
            };

            this.mockRepository.Setup(repo => repo.InsertNewBrand(brand)).ReturnsAsync(true);

            // Act
            var result = await brandRepository.InsertNewBrand(brand);

            // Assert
            Assert.True(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewBrand_NullBrand_ReturnsFalse()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            InsertBrandModel brand = null;

            this.mockRepository.Setup(repo => repo.InsertNewBrand(brand)).ReturnsAsync(false);

            // Act
            var result = await brandRepository.InsertNewBrand(brand);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateBrand_ValidBrand_ReturnsTrue()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            var brand = new UpdateBrandModel
            {
                BrandId = 1,
                BrandName = "Updated Brand",
                BrandLogo = "UpdatedLogoPath"
            };

            this.mockRepository.Setup(repo => repo.UpdateBrand(brand)).ReturnsAsync(true);

            // Act
            var result = await brandRepository.UpdateBrand(brand);

            // Assert
            Assert.True(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateBrand_NullBrand_ReturnsFalse()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            UpdateBrandModel brand = null;

            this.mockRepository.Setup(repo => repo.UpdateBrand(brand)).ReturnsAsync(false);

            // Act
            var result = await brandRepository.UpdateBrand(brand);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }
    }
}
