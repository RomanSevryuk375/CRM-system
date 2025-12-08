using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<SkillEntity>
{
    void IEntityTypeConfiguration<SkillEntity>.Configure(EntityTypeBuilder<SkillEntity> builder)
    {

        builder.ToTable("skills");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.WorkerId)
            .IsRequired();

        builder.Property(s => s.SpecializationId)
            .IsRequired();

        builder.HasOne(s => s.Worker)
            .WithMany(w => w.Skills)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Specialization)
            .WithMany(sp => sp.Skills)
            .HasForeignKey(s => s.SpecializationId)
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}
