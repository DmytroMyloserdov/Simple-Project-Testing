using System.Data.Entity;
using SqlServer.Entities;
using SqlServer.EntityConfiguration;

namespace SqlServer.Context
{
    public class SimpleContext : DbContext
    {
        public SimpleContext()
            : base("name=SimpleContext")
        {
        }

        public SimpleContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
        }
    }
}
