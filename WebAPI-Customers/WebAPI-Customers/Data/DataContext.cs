using Microsoft.EntityFrameworkCore;
using WebAPI_Customers.Entities;

namespace WebAPI_Customers.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOne(c => c.CustomerInfo)
                .WithOne(ci => ci.Customer)
                .HasForeignKey<CustomerInfo>(ci => ci.CustomerId);
        }
    }
}
