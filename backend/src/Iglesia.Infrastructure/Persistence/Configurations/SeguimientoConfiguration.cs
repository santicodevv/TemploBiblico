using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class SeguimientoConfiguration : IEntityTypeConfiguration<Seguimiento>
{
    public void Configure(EntityTypeBuilder<Seguimiento> builder)
    {
        builder.ToTable("Seguimientos");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Motivo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Observacion)
            .HasMaxLength(1000);

        builder.Property(s => s.Responsable)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(s => s.Miembro)
            .WithMany(m => m.Seguimientos)
            .HasForeignKey(s => s.MiembroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.MiembroId);
        builder.HasIndex(s => s.Fecha);
        builder.HasIndex(s => s.ProximaVisita);
    }
}
