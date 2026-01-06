// Ignore Spelling: Img

using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class AcceptanceImgConfiguration : IEntityTypeConfiguration<AcceptanceImgEntity>
{
    void IEntityTypeConfiguration<AcceptanceImgEntity>.Configure(EntityTypeBuilder<AcceptanceImgEntity> builder)
    {

        builder.ToTable("acceptance_imgs");

        builder.HasKey(x => x.Id);

        builder.Property(ai => ai.AcceptanceId)
            .IsRequired();

        builder.Property(ai => ai.FilePath)
            .HasMaxLength(ValidationConstants.MAX_PATH_LENGTH)
            .IsRequired();

        builder.Property(ai => ai.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired(false);

        builder.HasOne(ai => ai.Acceptance)
            .WithMany(a => a.Imgs)
            .HasForeignKey(ai => ai.AcceptanceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
