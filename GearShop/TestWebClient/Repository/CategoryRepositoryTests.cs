using BusinessObject.DTOS;
using Moq;
using Repository.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebClient.Repository
{
    public class CategoryRepositoryTests
    {
        private MockRepository mockRepository;



        public CategoryRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private CategoryRepository CreateCategoryRepository()
        {
            return new CategoryRepository();
        }

        [Fact]
        public async Task ChangeCategoryStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int CategoryId = 0;
            bool Status = false;

            // Act
            var result = await categoryRepository.ChangeCategoryStatus(
                CategoryId,
                Status);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetCategories_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();

            // Act
            var result = categoryRepository.GetCategories();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCategoryList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();

            // Act
            var result = await categoryRepository.GetCategoryList();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            InsertCategoryModel Category = null;

            // Act
            var result = await categoryRepository.InsertNewCategory(
                Category);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task IsKeywordExisted_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            string keyword = null;

            // Act
            var result = await categoryRepository.IsKeywordExisted(
                keyword);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            UpdateCategoryModel category = null;

            // Act
            var result = await categoryRepository.UpdateCategory(
                category);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
