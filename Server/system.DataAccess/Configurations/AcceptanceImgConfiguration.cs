using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class AcceptanceImgConfiguration : IEntityTypeConfiguration<AcceptanceImgEntity>
{
    void IEntityTypeConfiguration<AcceptanceImgEntity>.Configure(EntityTypeBuilder<AcceptanceImgEntity> builder)
    {

        builder.ToTable("acceptence_imgs");

        builder.HasKey(x => x.Id);

        builder.Property(ai => ai.AcceptanceId)
            .IsRequired();

        builder.Property(ai => ai.FilePath)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(ai => ai.Description)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.HasOne(ai => ai.Acceptance)
            .WithMany(a => a.Imgs)
            .HasForeignKey(ai => ai.AcceptanceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
