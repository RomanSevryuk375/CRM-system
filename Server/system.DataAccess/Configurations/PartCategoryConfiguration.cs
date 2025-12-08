using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PartCategoryConfiguration : IEntityTypeConfiguration<PartCategoryEntity>
{
    void IEntityTypeConfiguration<PartCategoryEntity>.Configure(EntityTypeBuilder<PartCategoryEntity> builder)
    {

        builder.ToTable("part_categories");

        builder.HasKey(x => x.Id);

        builder.Property(pc => pc.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(pc => pc.Description)
            .HasMaxLength(256)
            .IsRequired();

    }
}
