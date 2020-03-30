using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("Order Details");

            builder.HasKey(e => new { e.Id, e.ProductId }).HasName("PK_Order_Details");

            builder.HasIndex(e => e.Id).HasName("OrdersOrder_Details");
            builder.HasIndex(e => e.ProductId).HasName("ProductsOrder_Details");

            builder.Property(e => e.Id).HasColumnName("OrderID");
            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.Quantity).HasColumnName("Quantity").HasDefaultValueSql("((1))");
            builder.Property(e => e.UnitPrice).HasColumnName("UnitPrice").HasColumnType("money");

            builder.HasOne(d => d.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        }
    }
}