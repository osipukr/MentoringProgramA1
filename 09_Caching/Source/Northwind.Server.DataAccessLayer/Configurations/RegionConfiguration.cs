﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.DataAccessLayer.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");

            builder.HasKey(e => e.Id).IsClustered(false);

            builder.Property(e => e.Id).HasColumnName("RegionID").ValueGeneratedNever();
            builder.Property(e => e.RegionDescription).HasColumnName("RegionDescription").HasMaxLength(50).IsFixedLength().IsRequired();
        }
    }
}