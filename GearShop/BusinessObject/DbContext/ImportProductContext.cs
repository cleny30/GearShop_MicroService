using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Core
{
    public class ImportProductContext:DbContext
    {
        public ImportProductContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ImportProductContext"));
        }

        public virtual DbSet<ImportProduct> ImportProducts { get; set; }
        public virtual DbSet<ReceiptProduct> ReceiptProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiptProduct>()
            .HasOne(rp => rp.ImportProduct)
            .WithMany(ip => ip.ReceiptProducts)
            .HasForeignKey(rp => rp.ImportProductId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
