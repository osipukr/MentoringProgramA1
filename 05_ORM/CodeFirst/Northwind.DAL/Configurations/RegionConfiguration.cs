using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.DAL.Entities;

namespace Northwind.EF.DAL.Configuration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region");

            builder.HasKey(e => e.Id).IsClustered(false);

            builder.Property(e => e.Id).HasColumnName("RegionID").ValueGeneratedNever();
            builder.Property(e => e.RegionDescription).HasColumnName("RegionDescription").HasMaxLength(50).IsFixedLength().IsRequired();
        }
    }
}