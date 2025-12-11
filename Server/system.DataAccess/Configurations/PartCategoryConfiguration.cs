using CRMSystem.Core.Constants;
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
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.Property(pc => pc.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired();

    }
}
