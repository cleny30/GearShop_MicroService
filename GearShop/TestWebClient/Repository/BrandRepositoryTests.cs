using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class BrandRepositoryTests
    {
        private MockRepository mockRepository;



        public BrandRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private BrandRepository CreateBrandRepository()
        {
            return new BrandRepository();
        }

        [Fact]
        public void GetBrands_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();

            // Act
            var result = brandRepository.GetBrands();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetBrandList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();

            // Act
            var result = await brandRepository.GetBrandList();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeBrandStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            int BrandId = 0;
            bool Status = false;

            // Act
            var result = await brandRepository.ChangeBrandStatus(
                BrandId,
                Status);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewBrand_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            InsertBrandModel brand = null;

            // Act
            var result = await brandRepository.InsertNewBrand(
                brand);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateBrand_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var brandRepository = this.CreateBrandRepository();
            UpdateBrandModel brand = null;

            // Act
            var result = await brandRepository.UpdateBrand(
                brand);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
