using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("Credit Cards");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("CreditCardID");
            builder.Property(x => x.CardExpirationDate).HasColumnName("CardExpirationDate").HasColumnType("datetime");
            builder.Property(x => x.CardHolderName).HasColumnName("CardHolderName").HasMaxLength(20);
            builder.Property(x => x.EmployeeId).HasColumnName("EmployeeID");

            builder.HasOne(x => x.Employee)
                .WithOne(x => x.CreditCard)
                .HasForeignKey<Employee>(x => x.CreditCardId);
        }
    }
}