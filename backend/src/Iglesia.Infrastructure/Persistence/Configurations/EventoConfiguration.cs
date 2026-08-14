using Iglesia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iglesia.Infrastructure.Persistence.Configurations;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("Eventos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Descripcion)
            .HasMaxLength(1000);

        builder.Property(e => e.Responsable)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Ubicacion)
            .HasMaxLength(300);

        builder.HasIndex(e => e.Fecha);
    }
}
