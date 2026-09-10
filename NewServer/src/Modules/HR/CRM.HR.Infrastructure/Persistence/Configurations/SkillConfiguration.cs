using CRM.HR.Domain.Entities;
using CRM.HR.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.HR.Infrastructure.Persistence.Configurations;

internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills", HrDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new SkillId(value))
            .IsRequired();

        builder.Property(x => x.WorkerId)
            .HasConversion(
                id => id.Id,
                value => new WorkerId(value))
            .IsRequired();

        builder.Property(x => x.SpecializationId)
            .HasConversion(
                id => id.Id,
                value => new SpecializationId(value))
            .IsRequired();

        builder.HasOne<Specialization>()
            .WithMany()
            .HasForeignKey(x => x.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.SpecializationId);
        builder.HasIndex(x => new { x.WorkerId, x.SpecializationId }).IsUnique();
    }
}
