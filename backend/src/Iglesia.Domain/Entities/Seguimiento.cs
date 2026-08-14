using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Seguimiento
{
    public Guid Id { get; private set; }
    public Guid MiembroId { get; private set; }
    public DateOnly Fecha { get; private set; }
    public string Motivo { get; private set; } = string.Empty;
    public string? Observacion { get; private set; }
    public DateOnly? ProximaVisita { get; private set; }
    public string Responsable { get; private set; } = string.Empty;

    // Navigation properties
    public Miembro Miembro { get; private set; } = null!;

    private Seguimiento() { } // EF Core

    public Seguimiento(
        Guid miembroId,
        DateOnly fecha,
        string motivo,
        string responsable,
        string? observacion = null,
        DateOnly? proximaVisita = null)
    {
        if (miembroId == Guid.Empty)
            throw new DomainException("El miembro es requerido para el seguimiento.");

        if (string.IsNullOrWhiteSpace(motivo))
            throw new DomainException("El motivo del seguimiento es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del seguimiento es requerido.");

        if (proximaVisita.HasValue && proximaVisita.Value < fecha)
            throw new DomainException("La próxima visita no puede ser anterior a la fecha del seguimiento.");

        Id = Guid.NewGuid();
        MiembroId = miembroId;
        Fecha = fecha;
        Motivo = motivo;
        Responsable = responsable;
        Observacion = observacion;
        ProximaVisita = proximaVisita;
    }

    public void Actualizar(
        string motivo,
        string responsable,
        string? observacion,
        DateOnly? proximaVisita)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw new DomainException("El motivo del seguimiento es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del seguimiento es requerido.");

        if (proximaVisita.HasValue && proximaVisita.Value < Fecha)
            throw new DomainException("La próxima visita no puede ser anterior a la fecha del seguimiento.");

        Motivo = motivo;
        Responsable = responsable;
        Observacion = observacion;
        ProximaVisita = proximaVisita;
    }
}
