using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.DAL.Configurations
{
    public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
    {
        public void Configure(EntityTypeBuilder<Territory> builder)
        {
            builder.ToTable("Territories");

            builder.HasKey(e => e.Id).IsClustered(false);

            builder.Property(e => e.Id).HasColumnName("TerritoryID").HasMaxLength(20);
            builder.Property(e => e.RegionId).HasColumnName("RegionID");
            builder.Property(e => e.TerritoryDescription).HasColumnName("TerritoryDescription").HasMaxLength(50).IsFixedLength().IsRequired();

            builder.HasOne(d => d.Region)
                .WithMany(p => p.Territories)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Territories_Region");
        }
    }
}