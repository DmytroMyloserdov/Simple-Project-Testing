using SqlServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SqlServer.EntityConfiguration
{
    public class OrderEntityConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {
            ToTable("Orders");
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
