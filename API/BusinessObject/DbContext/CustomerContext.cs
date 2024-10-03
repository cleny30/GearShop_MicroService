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
    public  class CustomerContext : DbContext
    {
        public CustomerContext()
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CustomerContext"));
        }
    }
}
