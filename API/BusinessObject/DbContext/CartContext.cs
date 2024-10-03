using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Core
{
    public class CartContext:DbContext
    {
        public CartContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CartContext"));
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
             .HasOne(ci => ci.Cart)
             .WithMany(c => c.CartItems)
             .HasForeignKey(ci => ci.CartId);
        }
    }
}
