using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(order => order.Id);

            builder.HasIndex(order => order.CustomerId).HasName("CustomersOrders");
            builder.HasIndex(order => order.EmployeeId).HasName("EmployeesOrders");
            builder.HasIndex(order => order.OrderDate).HasName("OrderDate");
            builder.HasIndex(order => order.ShipPostalCode).HasName("ShipPostalCode");
            builder.HasIndex(order => order.ShipVia).HasName("ShippersOrders");
            builder.HasIndex(order => order.ShippedDate).HasName("ShippedDate");

            builder.Property(order => order.Id).HasColumnName("OrderID");
            builder.Property(order => order.CustomerId).HasColumnName("CustomerID").HasMaxLength(5).IsFixedLength();
            builder.Property(order => order.EmployeeId).HasColumnName("EmployeeID");
            builder.Property(order => order.OrderDate).HasColumnName("OrderDate").HasColumnType("datetime");
            builder.Property(order => order.RequiredDate).HasColumnName("RequiredDate").HasColumnType("datetime");
            builder.Property(order => order.ShippedDate).HasColumnName("ShippedDate").HasColumnType("datetime");
            builder.Property(order => order.ShipVia).HasColumnName("ShipVia");
            builder.Property(order => order.Freight).HasColumnName("Freight").HasColumnType("money").HasDefaultValueSql("((0))");
            builder.Property(order => order.ShipName).HasColumnName("ShipName").HasMaxLength(40);
            builder.Property(order => order.ShipAddress).HasColumnName("ShipAddress").HasMaxLength(60);
            builder.Property(order => order.ShipCity).HasColumnName("ShipCity").HasMaxLength(15);
            builder.Property(order => order.ShipCountry).HasColumnName("ShipCountry").HasMaxLength(15);
            builder.Property(order => order.ShipRegion).HasColumnName("ShipRegion").HasMaxLength(15);
            builder.Property(order => order.ShipPostalCode).HasColumnName("ShipPostalCode").HasMaxLength(10);

            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            builder.HasOne(order => order.Employee)
                .WithMany(employee => employee.Orders)
                .HasForeignKey(order => order.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");

            builder.HasOne(order => order.ShipViaNavigation)
                .WithMany(shipper => shipper.Orders)
                .HasForeignKey(order => order.ShipVia)
                .HasConstraintName("FK_Orders_Shippers");
        }
    }
}