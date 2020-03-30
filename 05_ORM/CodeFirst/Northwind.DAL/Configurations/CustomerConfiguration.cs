using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(customer => customer.Id);

            builder.HasIndex(customer => customer.City).HasName("City");
            builder.HasIndex(customer => customer.CompanyName).HasName("CompanyName");
            builder.HasIndex(customer => customer.PostalCode).HasName("PostalCode");
            builder.HasIndex(customer => customer.Region).HasName("Region");

            builder.Property(customer => customer.Id).HasColumnName("CustomerID").HasMaxLength(5).IsFixedLength();
            builder.Property(customer => customer.Address).HasColumnName("Address").HasMaxLength(60);
            builder.Property(customer => customer.City).HasColumnName("City").HasMaxLength(15);
            builder.Property(customer => customer.CompanyName).HasColumnName("CompanyName").HasMaxLength(40).IsRequired();
            builder.Property(customer => customer.ContactName).HasColumnName("ContactName").HasMaxLength(30);
            builder.Property(customer => customer.ContactTitle).HasColumnName("ContactTitle").HasMaxLength(30);
            builder.Property(customer => customer.Country).HasColumnName("Country").HasMaxLength(15);
            builder.Property(customer => customer.Fax).HasColumnName("Fax").HasMaxLength(24);
            builder.Property(customer => customer.Phone).HasColumnName("Phone").HasMaxLength(24);
            builder.Property(customer => customer.PostalCode).HasColumnName("PostalCode").HasMaxLength(10);
            builder.Property(customer => customer.Region).HasColumnName("Region").HasMaxLength(15);
            builder.Property(customer => customer.EstablishmentDate).HasColumnName("EstablishmentDate").HasColumnType("datetime");

            builder.HasMany(x => x.CustomerCustomerDemo)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}