using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.DataAccessLayer.Configurations
{
    public class CustomerDemographicsConfiguration : IEntityTypeConfiguration<CustomerDemographics>
    {
        public void Configure(EntityTypeBuilder<CustomerDemographics> builder)
        {
            builder.ToTable("CustomerDemographics");

            builder.HasKey(e => e.CustomerTypeId).IsClustered(false);

            builder.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID").HasMaxLength(10).IsFixedLength();
            builder.Property(e => e.CustomerDesc).HasColumnName("CustomerDesc").HasColumnType("ntext");
        }
    }
}
