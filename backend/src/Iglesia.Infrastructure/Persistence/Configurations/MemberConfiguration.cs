using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Phone)
            .HasMaxLength(20);

        builder.Property(m => m.Address)
            .HasMaxLength(500);

        builder.Property(m => m.Email)
            .HasMaxLength(256);

        builder.Property(m => m.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(m => m.Status)
            .HasConversion<int>();

        builder.HasOne(m => m.Ministry)
            .WithMany(min => min.Members)
            .HasForeignKey(m => m.MinistryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(m => m.Email);
        builder.HasIndex(m => m.Status);
    }
}
