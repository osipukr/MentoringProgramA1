using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.DataAccessLayer.Configurations
{
    public class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<CustomerCustomerDemo>
    {
        public void Configure(EntityTypeBuilder<CustomerCustomerDemo> builder)
        {
            builder.ToTable("CustomerCustomerDemo");

            builder.HasKey(e => new { e.CustomerId, e.CustomerTypeId }).IsClustered(false);

            builder.Property(e => e.CustomerId).HasColumnName("CustomerID").HasMaxLength(5).IsFixedLength();
            builder.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID").HasMaxLength(10).IsFixedLength();

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerCustomerDemo)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo_Customers");

            builder.HasOne(d => d.CustomerType)
                .WithMany(p => p.CustomerCustomerDemo)
                .HasForeignKey(d => d.CustomerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo");
        }
    }
}