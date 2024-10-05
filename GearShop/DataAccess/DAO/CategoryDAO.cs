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
    }
}
