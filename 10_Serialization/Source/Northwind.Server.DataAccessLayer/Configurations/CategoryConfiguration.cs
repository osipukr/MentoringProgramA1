using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Server.DataAccessLayer.Entities;

namespace Northwind.Server.DataAccessLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(category => category.Id);

            builder.HasIndex(category => category.CategoryName).HasName("CategoryName");

            builder.Property(category => category.Id).HasColumnName("CategoryID");
            builder.Property(category => category.CategoryName).HasColumnName("CategoryName").HasMaxLength(15).IsRequired();
            builder.Property(category => category.Description).HasColumnName("Description").HasColumnType("ntext");
            builder.Property(category => category.Picture).HasColumnName("Picture").HasColumnType("image");
        }
    }
}