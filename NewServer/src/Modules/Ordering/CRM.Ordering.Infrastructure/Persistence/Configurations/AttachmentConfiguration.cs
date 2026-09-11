using CRM.Ordering.Domain.Entities;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachments", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.UploadedBy).IsRequired();

        builder.Property(x => x.FileName)
            .HasMaxLength(Attachment.MaxFileNameLength)
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasMaxLength(Attachment.MaxFilePathLength)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(Attachment.MaxContentTypeLength)
            .IsRequired();

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Attachment.MaxDescriptionLength)
            .IsRequired(false);

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.UploadedBy);
    }
}
