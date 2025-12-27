using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PartConfiguration : IEntityTypeConfiguration<PartEntity>
{
    void IEntityTypeConfiguration<PartEntity>.Configure(EntityTypeBuilder<PartEntity> builder)
    {

        builder.ToTable("parts");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.CategoryId)
            .IsRequired();

        builder.Property(p => p.OEMArticle)
            .HasMaxLength(ValidationConstants.MAX_ARTICLE_LENGTH)
            .IsRequired(false);

        builder.Property(p => p.ManufacturerArticle)
            .HasMaxLength(ValidationConstants.MAX_ARTICLE_LENGTH)
            .IsRequired(false);

        builder.Property(p => p.InternalArticle)
            .HasMaxLength(ValidationConstants.MAX_ARTICLE_LENGTH)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(p => p.Manufacturer)
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Applicability)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired();

        builder.HasOne(p => p.PartCategory)
            .WithMany(pc => pc.Parts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
