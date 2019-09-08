using SqlServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SqlServer.EntityConfiguration
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductEntityConfiguration()
        {
            ToTable("Products");
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Description).HasMaxLength(500);
            Property(e => e.Manufacturer).HasMaxLength(50);
            Property(e => e.Name).HasMaxLength(50);

            HasMany(e => e.Orders).WithMany(e => e.Products);
        }
    }
}
