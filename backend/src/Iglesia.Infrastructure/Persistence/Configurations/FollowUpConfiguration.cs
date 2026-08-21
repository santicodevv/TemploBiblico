using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class FollowUpConfiguration : IEntityTypeConfiguration<FollowUp>
{
    public void Configure(EntityTypeBuilder<FollowUp> builder)
    {
        builder.ToTable("FollowUps");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Reason)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Notes)
            .HasMaxLength(1000);

        builder.Property(f => f.AssignedTo)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(f => f.Member)
            .WithMany(m => m.FollowUps)
            .HasForeignKey(f => f.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(f => f.MemberId);
        builder.HasIndex(f => f.Date);
        builder.HasIndex(f => f.NextVisitDate);
    }
}
