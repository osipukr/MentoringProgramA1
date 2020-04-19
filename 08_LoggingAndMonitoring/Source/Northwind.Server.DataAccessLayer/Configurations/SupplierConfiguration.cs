using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.DataAccessLayer.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.CompanyName).HasName("CompanyName");
            builder.HasIndex(e => e.PostalCode).HasName("PostalCode");

            builder.Property(e => e.Id).HasColumnName("SupplierID");
            builder.Property(e => e.Address).HasColumnName("Address").HasMaxLength(60);
            builder.Property(e => e.City).HasColumnName("City").HasMaxLength(15);
            builder.Property(e => e.CompanyName).HasColumnName("CompanyName").HasMaxLength(40).IsRequired();
            builder.Property(e => e.ContactName).HasColumnName("ContactName").HasMaxLength(30);
            builder.Property(e => e.ContactTitle).HasColumnName("ContactTitle").HasMaxLength(30);
            builder.Property(e => e.Country).HasColumnName("Country").HasMaxLength(15);
            builder.Property(e => e.Fax).HasColumnName("Fax").HasMaxLength(24);
            builder.Property(e => e.HomePage).HasColumnName("HomePage").HasColumnType("ntext");
            builder.Property(e => e.Phone).HasColumnName("Phone").HasMaxLength(24);
            builder.Property(e => e.PostalCode).HasColumnName("PostalCode").HasMaxLength(10);
            builder.Property(e => e.Region).HasColumnName("Region").HasMaxLength(15);
        }
    }
}