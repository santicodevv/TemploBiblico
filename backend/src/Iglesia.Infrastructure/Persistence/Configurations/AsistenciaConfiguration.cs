using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
{
    public void Configure(EntityTypeBuilder<Asistencia> builder)
    {
        builder.ToTable("Asistencias");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Estado)
            .HasConversion<int>();

        builder.Property(a => a.Observacion)
            .HasMaxLength(500);

        builder.HasOne(a => a.Miembro)
            .WithMany(m => m.Asistencias)
            .HasForeignKey(a => a.MiembroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Evento)
            .WithMany(e => e.Asistencias)
            .HasForeignKey(a => a.EventoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => new { a.MiembroId, a.EventoId })
            .IsUnique();

        builder.HasIndex(a => a.Fecha);
    }
}
