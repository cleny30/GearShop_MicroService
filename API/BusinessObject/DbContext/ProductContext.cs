using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Core
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProductContext"));
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
    }
}
