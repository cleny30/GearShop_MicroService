using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.SeedData
{
    public static class CategorySeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CateId = 1,
                    CateName = "Keyboard",
                    Keyword = "KB",
                    IsAvailable = true
                },
                new Category
                {
                    CateId = 2,
                    CateName = "Mouse",
                    Keyword = "MS",
                    IsAvailable = true
                },
                new Category
                {
                    CateId = 3,
                    CateName = "Headphone",
                    Keyword = "HP",
                    IsAvailable = true
                },
                new Category
                {
                    CateId = 4,
                    CateName = "Gaming Chairs",
                    Keyword = "GC",
                    IsAvailable = true
                },
                new Category
                {
                    CateId = 5,
                    CateName = "Controller",
                    Keyword = "CT",
                    IsAvailable = true
                }
            );
        }
    }
}
