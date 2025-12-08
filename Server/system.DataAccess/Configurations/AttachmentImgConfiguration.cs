using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class AttachmentImgConfiguration : IEntityTypeConfiguration<AttachmentImgEntity>
{
    void IEntityTypeConfiguration<AttachmentImgEntity>.Configure(EntityTypeBuilder<AttachmentImgEntity> builder)
    {

        builder.ToTable("attachment_imgs");

        builder.HasKey(x => x.Id);
        
        builder.Property(ai => ai.AttachmentId)
            .IsRequired();

        builder.Property(ai => ai.FilePath)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(ai => ai.Description)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.HasOne(ai => ai.Attachment)
            .WithMany(a => a.Imgs)
            .HasForeignKey(ai => ai.AttachmentId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
