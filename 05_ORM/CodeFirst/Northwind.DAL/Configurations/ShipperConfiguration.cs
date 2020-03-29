using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.EF.DAL.Configuration
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shippers");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ShipperID");
            builder.Property(e => e.CompanyName).HasColumnName("CompanyName").HasMaxLength(40).IsRequired();
            builder.Property(e => e.Phone).HasColumnName("Phone").HasMaxLength(24);
        }
    }
}