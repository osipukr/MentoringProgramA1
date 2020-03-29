using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.EF.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.CategoryId).HasName("CategoryID");
            builder.HasIndex(e => e.ProductName).HasName("ProductName");
            builder.HasIndex(e => e.SupplierId).HasName("SuppliersProducts");

            builder.Property(e => e.Id).HasColumnName("ProductID");
            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.ProductName).HasColumnName("ProductName").HasMaxLength(40).IsRequired();
            builder.Property(e => e.QuantityPerUnit).HasColumnName("QuantityPerUnit").HasMaxLength(20);
            builder.Property(e => e.ReorderLevel).HasColumnName("ReorderLevel").HasDefaultValueSql("((0))");
            builder.Property(e => e.SupplierId).HasColumnName("SupplierID");
            builder.Property(e => e.UnitPrice).HasColumnName("UnitPrice").HasColumnType("money").HasDefaultValueSql("((0))");
            builder.Property(e => e.UnitsInStock).HasColumnName("UnitsInStock").HasDefaultValueSql("((0))");
            builder.Property(e => e.UnitsOnOrder).HasColumnName("UnitsOnOrder").HasDefaultValueSql("((0))");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            builder.HasOne(d => d.Supplier)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        }
    }
}