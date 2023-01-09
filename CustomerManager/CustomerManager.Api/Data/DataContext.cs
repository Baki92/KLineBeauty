using CustomerManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CustomerManager.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(customer => customer.Identifier).IsUnique();
                
        }
    }
}
