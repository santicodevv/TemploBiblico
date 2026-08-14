using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class MinisterioConfiguration : IEntityTypeConfiguration<Ministerio>
{
    public void Configure(EntityTypeBuilder<Ministerio> builder)
    {
        builder.ToTable("Ministerios");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Responsable)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Descripcion)
            .HasMaxLength(500);

        builder.HasIndex(m => m.Nombre)
            .IsUnique();
    }
}
