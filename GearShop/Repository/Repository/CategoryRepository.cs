using BusinessObject.Models.Entity;
using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<bool> ChangeCategoryStatus(int CategoryId, bool Status)
        => await CategoryDAO.ChangeCategoryStatus(CategoryId, Status);
        public List<Category> GetCategories()=>CategoryDAO.GetCategories();
        public async Task<List<CategoryModel>> GetCategoryList()
        => await CategoryDAO.GetCategoryList();
        public async Task<bool> InsertNewCategory(InsertCategoryModel Category)
        => await CategoryDAO.InsertNewCategory(Category);
        public async Task<bool> IsKeywordExisted(string keyword)
        => await CategoryDAO.IsKeywordExisted(keyword);
        public async Task<bool> UpdateCategory(UpdateCategoryModel category)
        => await CategoryDAO.UpdateCategory(category);
    }
}
