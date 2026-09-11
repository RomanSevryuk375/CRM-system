using CRM.HR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.HR.Infrastructure.Persistence.Configurations;

internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills", HrDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.WorkerId).IsRequired();
        builder.Property(x => x.SpecializationId).IsRequired();

        builder.HasOne<Specialization>()
            .WithMany()
            .HasForeignKey(x => x.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.SpecializationId);
        builder.HasIndex(x => new { x.WorkerId, x.SpecializationId }).IsUnique();
    }
}
