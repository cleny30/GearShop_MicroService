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
        public List<Category> GetCategories()=>CategoryDAO.GetCategories();
        public async Task<List<CategoryModel>> GetCategoryList()
        => await CategoryDAO.GetCategoryList();
    }
}
