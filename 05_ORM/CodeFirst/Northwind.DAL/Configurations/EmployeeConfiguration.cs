using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(employee => employee.Id);

            builder.HasIndex(employee => employee.LastName).HasName("LastName");
            builder.HasIndex(employee => employee.PostalCode).HasName("PostalCode");

            builder.Property(employee => employee.Id).HasColumnName("EmployeeID");
            builder.Property(employee => employee.Address).HasColumnName("Address").HasMaxLength(60);
            builder.Property(employee => employee.BirthDate).HasColumnName("BirthDate").HasColumnType("datetime");
            builder.Property(employee => employee.City).HasColumnName("City").HasMaxLength(15);
            builder.Property(employee => employee.Country).HasColumnName("Country").HasMaxLength(15);
            builder.Property(employee => employee.Extension).HasColumnName("Extension").HasMaxLength(4);
            builder.Property(employee => employee.FirstName).HasColumnName("FirstName").HasMaxLength(10).IsRequired();
            builder.Property(employee => employee.HireDate).HasColumnName("HireDate").HasColumnType("datetime");
            builder.Property(employee => employee.HomePhone).HasColumnName("HomePhone").HasMaxLength(24);
            builder.Property(employee => employee.LastName).HasColumnName("LastName").HasMaxLength(20).IsRequired();
            builder.Property(employee => employee.Notes).HasColumnName("Notes").HasColumnType("ntext");
            builder.Property(employee => employee.Photo).HasColumnName("Photo").HasColumnType("image");
            builder.Property(employee => employee.PhotoPath).HasColumnName("PhotoPath").HasMaxLength(255);
            builder.Property(employee => employee.PostalCode).HasColumnName("PostalCode").HasMaxLength(10);
            builder.Property(employee => employee.Region).HasColumnName("Region").HasMaxLength(15);
            builder.Property(employee => employee.Title).HasColumnName("Title").HasMaxLength(30);
            builder.Property(employee => employee.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").HasMaxLength(25);
            builder.Property(employee => employee.CreditCardId).HasColumnName("CreditCardID");

            builder.HasOne(employee => employee.ReportsToNavigation)
                .WithMany(employee => employee.InverseReportsToNavigation)
                .HasForeignKey(employee => employee.ReportsTo)
                .HasConstraintName("FK_Employees_Employees");

            builder.HasOne(employee => employee.CreditCard)
                .WithOne(creditCard => creditCard.Employee)
                .HasForeignKey<CreditCard>(creditCard => creditCard.EmployeeId);
        }
    }
}