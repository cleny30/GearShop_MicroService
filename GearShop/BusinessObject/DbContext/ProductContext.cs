using BusinessObject.Models.Entity;
using BusinessObject.SeedData;
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
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Define composite key for ProductAttribute
            modelBuilder.Entity<ProductAttribute>()
                .HasKey(pa => new { pa.ProId, pa.Feature });

            modelBuilder.Entity<ProductImage>()
                .HasKey(pa => new { pa.ProId, pa.ProImg });

            // Define relationships and seed data
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(pa => pa.ProId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProId)
                .OnDelete(DeleteBehavior.Cascade);

            BrandSeedData.Seed(modelBuilder);

            CategorySeedData.Seed(modelBuilder);

            ProductSeedData.Seed(modelBuilder);

            ProductAttributeSeedData.Seed(modelBuilder);

            ProductImageSeedData.Seed(modelBuilder);
        }
    }
}
