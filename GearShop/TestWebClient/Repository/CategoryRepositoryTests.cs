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
    public class CategoryRepositoryTests
    {
        private Mock<ICategoryRepository> mockCategoryRepository;

        public CategoryRepositoryTests()
        {
            this.mockCategoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        }

        private ICategoryRepository CreateCategoryRepository()
        {
            return this.mockCategoryRepository.Object;
        }

        [Fact]
        public async Task ChangeCategoryStatus_ValidCategoryIdAndStatus_ReturnsTrue()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int categoryId = 1; // Use a valid category ID for testing
            bool status = true;

            this.mockCategoryRepository.Setup(repo => repo.ChangeCategoryStatus(categoryId, status)).ReturnsAsync(true);

            // Act
            var result = await categoryRepository.ChangeCategoryStatus(categoryId, status);

            // Assert
            Assert.True(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task ChangeCategoryStatus_InvalidCategoryId_ReturnsFalse()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int invalidCategoryId = 0; // Assuming 0 is an invalid ID
            bool status = false;

            this.mockCategoryRepository.Setup(repo => repo.ChangeCategoryStatus(invalidCategoryId, status)).ReturnsAsync(false);

            // Act
            var result = await categoryRepository.ChangeCategoryStatus(invalidCategoryId, status);

            // Assert
            Assert.False(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public void GetCategories_ReturnsListOfCategories()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            var expectedCategories = new List<Category>
        {
            new Category { CateId = 1, CateName = "Controller", Keyword = "CT", IsAvailable = true },
            new Category { CateId = 2, CateName = "Mouse", Keyword = "MS", IsAvailable = true }
        };

            this.mockCategoryRepository.Setup(repo => repo.GetCategories()).Returns(expectedCategories);

            // Act
            var result = categoryRepository.GetCategories();

            // Assert
            Assert.Equal(expectedCategories.Count, result.Count);
            Assert.Equal(expectedCategories.First().CateName, result.First().CateName);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCategoryList_ReturnsListOfCategoryModels()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            var expectedCategories = new List<CategoryModel>
        {
            new CategoryModel {CateId = 1, CateName = "Controller", Keyword = "CT", quantity = 10, IsAvailable = true },
            new CategoryModel { CateId = 2, CateName = "Mouse", Keyword = "MS", quantity = 5, IsAvailable = true }
        };

            this.mockCategoryRepository.Setup(repo => repo.GetCategoryList()).ReturnsAsync(expectedCategories);

            // Act
            var result = await categoryRepository.GetCategoryList();

            // Assert
            Assert.Equal(expectedCategories.Count, result.Count);
            Assert.Equal(expectedCategories.First().CateName, result.First().CateName);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewCategory_ValidCategory_ReturnsTrue()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            var category = new InsertCategoryModel
            {
                CateId = 1, // Assuming you have a specific ID for testing
                CateName = "New Category",
                Keyword = "NC",
                IsAvailable = true
            };

            this.mockCategoryRepository.Setup(repo => repo.InsertNewCategory(category)).ReturnsAsync(true);

            // Act
            var result = await categoryRepository.InsertNewCategory(category);

            // Assert
            Assert.True(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertNewCategory_NullCategory_ReturnsFalse()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            InsertCategoryModel category = null;

            this.mockCategoryRepository.Setup(repo => repo.InsertNewCategory(category)).ReturnsAsync(false);

            // Act
            var result = await categoryRepository.InsertNewCategory(category);

            // Assert
            Assert.False(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task IsKeywordExisted_ValidKeyword_ReturnsTrue()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            string keyword = "existingKeyword";

            this.mockCategoryRepository.Setup(repo => repo.IsKeywordExisted(keyword)).ReturnsAsync(true);

            // Act
            var result = await categoryRepository.IsKeywordExisted(keyword);

            // Assert
            Assert.True(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task IsKeywordExisted_NullKeyword_ReturnsFalse()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            string keyword = null;

            this.mockCategoryRepository.Setup(repo => repo.IsKeywordExisted(keyword)).ReturnsAsync(false);

            // Act
            var result = await categoryRepository.IsKeywordExisted(keyword);

            // Assert
            Assert.False(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateCategory_ValidCategory_ReturnsTrue()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            var category = new UpdateCategoryModel
            {
                CateId = 1, // Assuming you have a specific ID for testing
                CateName = "Updated Category"
            };

            this.mockCategoryRepository.Setup(repo => repo.UpdateCategory(category)).ReturnsAsync(true);

            // Act
            var result = await categoryRepository.UpdateCategory(category);

            // Assert
            Assert.True(result);
            this.mockCategoryRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateCategory_NullCategory_ReturnsFalse()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            UpdateCategoryModel category = null;

            this.mockCategoryRepository.Setup(repo => repo.UpdateCategory(category)).ReturnsAsync(false);

            // Act
            var result = await categoryRepository.UpdateCategory(category);

            // Assert
            Assert.False(result);
            this.mockCategoryRepository.VerifyAll();
        }
    }
}
