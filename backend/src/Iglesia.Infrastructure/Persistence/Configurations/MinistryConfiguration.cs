using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class MinistryConfiguration : IEntityTypeConfiguration<Ministry>
{
    public void Configure(EntityTypeBuilder<Ministry> builder)
    {
        builder.ToTable("Ministries");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Leader)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .HasMaxLength(500);

        builder.HasIndex(m => m.Name)
            .IsUnique();
    }
}
