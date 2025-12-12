using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentEntity>
{
    void IEntityTypeConfiguration<AttachmentEntity>.Configure(EntityTypeBuilder<AttachmentEntity> builder)
    {

        builder.ToTable("attachments");

        builder.HasKey(x => x.Id); 

        builder.Property(a => a.OrderId)
            .IsRequired();

        builder.Property(a => a.WorkerId)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.Property(a => a.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired(false);

        builder.HasOne(a => a.Order)
            .WithMany(o => o.Attachments)
            .HasForeignKey(a => a.OrderId) 
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Worker)
            .WithMany(w => w.Attachments)
            .HasForeignKey(a => a.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
