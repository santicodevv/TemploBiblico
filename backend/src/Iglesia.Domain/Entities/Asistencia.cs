using Iglesia.Domain.Enums;
using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Asistencia
{
    public Guid Id { get; private set; }
    public Guid MiembroId { get; private set; }
    public Guid EventoId { get; private set; }
    public DateOnly Fecha { get; private set; }
    public EstadoAsistencia Estado { get; private set; }
    public string? Observacion { get; private set; }

    // Navigation properties
    public Miembro Miembro { get; private set; } = null!;
    public Evento Evento { get; private set; } = null!;

    private Asistencia() { } // EF Core

    public Asistencia(
        Guid miembroId,
        Guid eventoId,
        DateOnly fecha,
        EstadoAsistencia estado,
        string? observacion = null)
    {
        if (miembroId == Guid.Empty)
            throw new DomainException("El miembro es requerido para registrar asistencia.");

        if (eventoId == Guid.Empty)
            throw new DomainException("El evento es requerido para registrar asistencia.");

        Id = Guid.NewGuid();
        MiembroId = miembroId;
        EventoId = eventoId;
        Fecha = fecha;
        Estado = estado;
        Observacion = observacion;
    }

    public void CambiarEstado(EstadoAsistencia estado, string? observacion = null)
    {
        Estado = estado;
        Observacion = observacion;
    }
}
