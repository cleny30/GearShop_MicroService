using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new ProductContext())
                {
                    list = context.Categorys.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static async Task<List<CategoryModel>> GetCategoryList()
        {
            List<Category> categories;
            try
            {
                using (var dbContext = new ProductContext())
                {
                    categories = await dbContext.Categorys.ToListAsync(); // Use ToListAsync for asynchronous operation
                }

                List<CategoryModel> categoryModels = new List<CategoryModel>();

                foreach (var item in categories)
                {
                    CategoryModel categoryModel = new CategoryModel();
                    categoryModel.CopyProperties(item);
                    categoryModels.Add(categoryModel);
                }
                return categoryModels;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return null;
            }
        }

        public static async Task<bool> IsKeywordExisted(string keyword)
        {
            try
            {
                using (var dbContext = new ProductContext())
                {
                    Category category = await dbContext.Categorys.FirstOrDefaultAsync(c => c.Keyword == keyword);
                    if(category == null)
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return false;
            }
        }

        public static async Task<bool> ChangeCategoryStatus(int CategoryId, bool Status)
        {
            try
            {
                using (var dbContext = new ProductContext())
                {
                    Category Category = dbContext.Categorys.FirstOrDefault(b => b.CateId == CategoryId);
                    if (Category == null)
                    {
                        // Optionally log that the Category was not found
                        return false;
                    }
                    Category.IsAvailable = Status;
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> InsertNewCategory(InsertCategoryModel Category)
        {
            try
            {
                Category _Category = new Category();
                using (var dbContext = new ProductContext())
                {
                    var lastCategory = await dbContext.Categorys
                    .OrderByDescending(b => b.CateId)
                    .FirstOrDefaultAsync();

                    int CategoryId = lastCategory.CateId + 1;

                    Category.CateId = CategoryId;

                    _Category.CopyProperties(Category);
                    await dbContext.AddAsync(_Category);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> UpdateCategory(UpdateCategoryModel category)
        {
            try
            {
                if (category == null)
                {
                    // Optionally log that the input Category model is null
                    return false;
                }
                using (var dbContext = new ProductContext())
                {
                    Category _Category = dbContext.Categorys.FirstOrDefault(b => b.CateId == category.CateId);
                    if (_Category == null)
                    {
                        // Optionally log that the Category was not found
                        return false;
                    }

                    _Category.CateName = category.CateName;
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
