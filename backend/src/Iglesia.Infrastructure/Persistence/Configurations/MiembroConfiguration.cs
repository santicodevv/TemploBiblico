using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class MiembroConfiguration : IEntityTypeConfiguration<Miembro>
{
    public void Configure(EntityTypeBuilder<Miembro> builder)
    {
        builder.ToTable("Miembros");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombres)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Apellidos)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Telefono)
            .HasMaxLength(20);

        builder.Property(m => m.Direccion)
            .HasMaxLength(500);

        builder.Property(m => m.Email)
            .HasMaxLength(256);

        builder.Property(m => m.Foto)
            .HasMaxLength(500);

        builder.Property(m => m.Estado)
            .HasConversion<int>();

        builder.HasOne(m => m.Ministerio)
            .WithMany(min => min.Miembros)
            .HasForeignKey(m => m.MinisterioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(m => m.Email);
        builder.HasIndex(m => m.Estado);
    }
}
