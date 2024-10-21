using BusinessObject.DTOS;
using BusinessObject.Models.Entity;

namespace Repository.IRepository
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Task<List<CategoryModel>> GetCategoryList();
        public Task<bool> IsKeywordExisted(string keyword);
        public Task<bool> ChangeCategoryStatus(int CategoryId, bool Status);
        public Task<bool> InsertNewCategory(InsertCategoryModel Category);
        public Task<bool> UpdateCategory(UpdateCategoryModel category);
    }
}
